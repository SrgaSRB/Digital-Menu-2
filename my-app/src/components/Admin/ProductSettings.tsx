import React, { useEffect, useState } from "react";
import api from "../../services/api";
import Loader from "../shared/Loader";

interface Subcategory {
    id: string;
    name: string;
    isSelected?: boolean;
  }

interface Product {
    id: string;
    name: string;
    description: string;
    additionalDescription: string;
    price: number;
    imageUrl: string;
    isDeleted: boolean;
    haveImage?: boolean;
    tempImageFile?: File;
    selectedSubCategories: Subcategory[];
    allSubCategories: Subcategory[];
}

const ProductSettings: React.FC = () => {

    const [productList, setProductList] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);

    const [filterName, setFilterName] = useState("");
    const [filterDescription, setFilterDescription] = useState("");
    const [filterMinPrice, setFilterMinPrice] = useState("");
    const [filterMaxPrice, setFilterMaxPrice] = useState("");

    const [subCategories, setSubCategories] = useState<Subcategory[]>([]);
    const [newProductName, setNewProductName] = useState("");
    const [newProductDescription, setNewProductDescription] = useState("");
    const [newProductAdditionalDescription, setNewProductAdditionalDescription] = useState("");
    const [newProductPrice, setNewProductPrice] = useState("");
    const [newProductImage, setNewProductImage] = useState<File | null>(null);

    useEffect(() => {

        const fetchProducts = async () => {
            setLoading(true);
            const user = localStorage.getItem("user");

            if (!user) {
                return
            }

            try {
                const parsedUser = JSON.parse(user);
                const adminId = parsedUser.id;

                const response = await api.get(`/product/admin/${adminId}`);
                setProductList(response.data);
            } catch (error) {
                console.error("Error fetching local data:", error);
            }
            finally {
                setLoading(false);
            }
        };

        fetchProducts();
    }, []);
    

    useEffect(() => {

        const fetchSubCategories = async () => {
            setLoading(true);
            const user = localStorage.getItem("user");

            if (!user) {
                return
            }

            try {
                const parsedUser = JSON.parse(user);
                const adminId = parsedUser.id;

                const response = await api.get(`/categories/all/${adminId}`);
                setSubCategories(response.data);
            } catch (error) {
                console.error("Error fetching local data:", error);
            }
            finally {
                setLoading(false);
            }
        };

        fetchSubCategories();
    }, []);


    const handleInputChange = (index: number, field: keyof Product, value: any) => {
        const updatedProducts = [...productList];
        (updatedProducts[index] as any)[field] = value;
        setProductList(updatedProducts);
    };

    const handleSave = async (product: Product) => {
        try {
            let imageUrl = product.imageUrl;

            if (product.tempImageFile) {
                const formData = new FormData();
                formData.append("file", product.tempImageFile);
                formData.append("upload_preset", "unsigned_preset"); // tvoj upload preset

                const response = await fetch("https://api.cloudinary.com/v1_1/dloemc8rb/image/upload", {
                    method: "POST",
                    body: formData,
                });

                const data = await response.json();
                if (data.secure_url) {
                    imageUrl = data.secure_url;
                    product.imageUrl = imageUrl;
                }
            }

            console.log(imageUrl)

            await api.put(`/product/${product.id}`, {
                name: product.name,
                description: product.description,
                additionalDescription: product.additionalDescription,
                price: product.price,
                imageUrl: product.imageUrl ? product.imageUrl : "",
                isDeleted: product.isDeleted,
                selectedSubCategoryIds: product.selectedSubCategories.map(sc => sc.id)
            });

            alert("Uspešno sačuvano!");

            // Očisti temp fajl iz memorije
            const updatedProducts = [...productList];
            const index = updatedProducts.findIndex(p => p.id === product.id);
            if (index !== -1) {
                updatedProducts[index].tempImageFile = undefined;
                updatedProducts[index].imageUrl = imageUrl;
            }
            setProductList(updatedProducts);

        } catch (error) {
            console.error("Greška pri čuvanju proizvoda:", error);
            alert("Greška pri čuvanju!");
        }
    };


    const handleDelete = async (productId: string) => {
        if (!window.confirm("Da li ste sigurni da želite da obrišete proizvod?")) return;
        try {
            await api.delete(`/product/${productId}`);
            setProductList((prev) => prev.filter(p => p.id !== productId));
            alert("Proizvod uspešno obrisan!");
        } catch (error) {
            console.error("Greška pri brisanju proizvoda:", error);
            alert("Greška pri brisanju!");
        }
    };

    const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>, index: number) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const updatedProducts = [...productList];
        updatedProducts[index].tempImageFile = file;
        setProductList(updatedProducts);
    };

    const handleDeleteImage = (index: number) => {
        const updatedProducts = [...productList];
        updatedProducts[index].imageUrl = '';
        updatedProducts[index].haveImage = false;
        updatedProducts[index].tempImageFile = undefined;
        setProductList(updatedProducts);
    };

    const handleSubcategoryChange = (productIndex: number, subcategory: Subcategory, checked: boolean) => {
        const updatedProducts = [...productList];
        const product = updatedProducts[productIndex];

        if (checked) {
            // Dodaj ako nije već dodat
            if (!product.selectedSubCategories.some(sc => sc.id === subcategory.id)) {
                product.selectedSubCategories.push({ id: subcategory.id, name: subcategory.name });
            }
        } else {
            // Ukloni
            product.selectedSubCategories = product.selectedSubCategories.filter(sc => sc.id !== subcategory.id);
        }

        setProductList(updatedProducts);
    };

    const handleCreateProduct = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
    
        const user = localStorage.getItem("user");
        if (!user) return;
    
        const parsedUser = JSON.parse(user);
        const adminId = parsedUser.id;
    
        try {
            let uploadedImageUrl = "";
    
            // Ako ima slike, uploaduj sliku na Cloudinary
            if (newProductImage) {
                const formData = new FormData();
                formData.append("file", newProductImage);
                formData.append("upload_preset", "unsigned_preset");
    
                const response = await fetch("https://api.cloudinary.com/v1_1/dloemc8rb/image/upload", {
                    method: "POST",
                    body: formData,
                });
    
                const data = await response.json();
                if (data.secure_url) {
                    uploadedImageUrl = data.secure_url;
                }
            }
    
            const selectedSubcategoryIds = subCategories
                .filter(sc => sc.isSelected)
                .map(sc => sc.id);
    
            await api.post("/product", {
                name: newProductName,
                description: newProductDescription,
                additionalDescription: newProductAdditionalDescription,
                price: parseFloat(newProductPrice),
                imageUrl: uploadedImageUrl,
                isDeleted: false,
                adminId: adminId,
                selectedSubCategoryIds: selectedSubcategoryIds
            });
    
            alert("Proizvod uspešno dodat!");
    
            setNewProductName("");
            setNewProductDescription("");
            setNewProductAdditionalDescription("");
            setNewProductPrice("");
            setNewProductImage(null);
            setSubCategories(prev => prev.map(sc => ({ ...sc, isSelected: false })));
    
        } catch (error) {
            console.error("Greška pri kreiranju proizvoda:", error);
            alert("Greška pri kreiranju proizvoda!");
        }
    };
    


    if (loading) {
        return <Loader />
    }


    return (
        <section className="items-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="items-wrapper">
                    <div className="div-block">
                        <div className="item-form create-if w-form">
                            <form id="wf-form-Create-item-form"  className="form create-f" onSubmit={handleCreateProduct}>
                                <div className="text-block-2">Dodaj novi proizvod</div>
                                <label htmlFor="name">Naziv*</label>
                                <input
                                    className="text-field-4 w-input"
                                    maxLength={256}
                                    name="name"
                                    data-name="Name"
                                    placeholder="Unesite naziv proizvoda"
                                    type="text"
                                    id="name"
                                    required
                                    value={newProductName}
                                    onChange={(e) => setNewProductName(e.target.value)}
                                />
                                <label htmlFor="field">Opis*</label>
                                <textarea
                                    id="field-4"
                                    name="field-4"
                                    maxLength={256}
                                    data-name="Field 4"
                                    placeholder="Unestite opis proizvoda (npr. sastojci)"
                                    className="textarea-4 w-input"
                                    value={newProductDescription}
                                    onChange={(e) => setNewProductDescription(e.target.value)}
                                ></textarea>
                                <label htmlFor="field">Dodatni opis</label>
                                <textarea
                                    id="field-5"
                                    name="field-5"
                                    maxLength={256}
                                    data-name="Field 5"
                                    placeholder="Unesite dodatni opis proizvoda (npr. obim kod pica)"
                                    className="textarea-2 w-input"
                                    value={newProductAdditionalDescription}
                                    onChange={(e) => setNewProductAdditionalDescription(e.target.value)}
                                ></textarea>
                                <label htmlFor="field">Cena*</label>
                                <input
                                    className="text-field-4 w-input"
                                    maxLength={256}
                                    name="field-2"
                                    data-name="Field 2"
                                    placeholder="Unesite cenu u dinarima"
                                    type="text"
                                    id="field-2"
                                    required
                                    value={newProductPrice}
                                    onChange={(e) => setNewProductPrice(e.target.value)}
                                />
                                <div>
                                    <label htmlFor="field-9">Slika</label>
                                    <div className="div-block-23">
                                        <input
                                            type="file"
                                            onChange={(e) => setNewProductImage(e.target.files?.[0] || null)}
                                        />
                                        <a
                                            className="button-7 w-button"
                                            onClick={() => setNewProductImage(null)}
                                        >
                                            Obriši sliku
                                        </a>
                                    </div>
                                </div>
                                <label htmlFor="">Odaberi podkategoriju</label>
                                <div className="div-block-2">
                                    {subCategories.map((subcategory) => (
                                        <label key={subcategory.id} className="w-checkbox">
                                            <input
                                                type="checkbox"
                                                name="checkbox"
                                                id={`checkbox-${subcategory.id}`}
                                                data-name="Checkbox"
                                                className="w-checkbox-input"
                                                onChange={(e) => {
                                                    const isChecked = e.target.checked;
                                                    setSubCategories((prev) =>
                                                        prev.map((sc) =>
                                                            sc.id === subcategory.id
                                                                ? { ...sc, isSelected: isChecked }
                                                                : sc
                                                        )
                                                    );
                                                }}
                                            />
                                            <span className="w-form-label">{subcategory.name}</span>
                                        </label>
                                    ))}

                                </div>
                                <input type="submit" data-wait="Please wait..." className="submit-button w-button" value="Napravi" />
                            </form>
                        </div>
                    </div>
                    <div className="div-block-3">
                        <div className="form-block w-form">
                            <form id="wf-form-Search-form" name="wf-form-Search-form" data-name="Search form" method="get" className="search-form" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="34c0e6c9-6c20-505e-a373-be0eced873f3" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                <div className="text-block-3">Filtriraj prikaz svih proizvoda</div>
                                <div className="div-block-8">
                                    <div className="div-block-6">
                                        <label htmlFor="name" className="field-label">Naziv</label>
                                        <input className="text-field w-input" maxLength={256} name="name-4" data-name="Name 4" placeholder="Unesite rec u nazivu za filtriranje" type="text" id="name-4"
                                            value={filterName}
                                            onChange={(e) => setFilterName(e.target.value)}
                                        />
                                    </div>
                                    <div className="div-block-6">
                                        <label htmlFor="field" className="field-label">Opis</label>
                                        <textarea id="field-6" name="field-6" maxLength={256} data-name="Field 6" placeholder="Unesite rec u opisu za filtriranje" className="textarea-3 text-field w-input"
                                            value={filterDescription}
                                            onChange={(e) => setFilterDescription(e.target.value)}
                                        />
                                    </div>
                                </div>
                                <div className="div-block-5">
                                    <div className="div-block-6">
                                        <label htmlFor="field" className="field-label">Min Cena</label>
                                        <input className="text-field w-input" maxLength={256} name="field-7" data-name="Field 7" placeholder="Unesi minimalnu cenu" type="number" id="field-7"
                                            value={filterMinPrice}
                                            onChange={(e) => setFilterMinPrice(e.target.value)}
                                        />
                                    </div>
                                    <div className="div-block-6">
                                        <label htmlFor="field" className="field-label">Max Cena</label>
                                        <input className="text-field w-input" maxLength={256} name="field-7" data-name="Field 7" placeholder="Unesi maximalnu cenu" type="number" id="field-7"
                                            value={filterMaxPrice}
                                            onChange={(e) => setFilterMaxPrice(e.target.value)}
                                        />
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div className="intems-list-div">
                            {productList
                                .filter((product) => {
                                    const matchesName = filterName ? product.name.toLowerCase().includes(filterName.toLowerCase()) : true;
                                    const matchesDescription = filterDescription ? product.description.toLowerCase().includes(filterDescription.toLowerCase()) : true;
                                    const matchesMinPrice = filterMinPrice ? product.price >= parseFloat(filterMinPrice) : true;
                                    const matchesMaxPrice = filterMaxPrice ? product.price <= parseFloat(filterMaxPrice) : true;

                                    return matchesName && matchesDescription && matchesMinPrice && matchesMaxPrice;
                                })
                                .map((product, index) => (
                                    <div className="item-form w-form">
                                        <form key={product.id} className="form">
                                            <div className="div-block-24">
                                                <label className="w-checkbox checkbox-field">
                                                    <input
                                                        type="checkbox"
                                                        checked={product.isDeleted}
                                                        onChange={(e) => handleInputChange(index, 'isDeleted', e.target.checked)}
                                                        className="w-checkbox-input checkbox"
                                                    />
                                                    <span className="checkbox-label w-form-label">Sakrij proizvod</span>
                                                </label>
                                            </div>

                                            <label>Naziv*</label>
                                            <input
                                                className="text-field-5 w-input"
                                                type="text"
                                                value={product.name}
                                                onChange={(e) => handleInputChange(index, 'name', e.target.value)}
                                            />

                                            <label>Opis*</label>
                                            <input
                                                className="text-field-5 w-input"
                                                type="text"
                                                value={product.description}
                                                onChange={(e) => handleInputChange(index, 'description', e.target.value)}
                                            />

                                            <label>Dodatni opis</label>
                                            <input
                                                className="text-field-5 w-input"
                                                type="text"
                                                value={product.additionalDescription}
                                                onChange={(e) => handleInputChange(index, 'additionalDescription', e.target.value)}
                                            />

                                            <label>Cena*</label>
                                            <input
                                                className="text-field-5 w-input"
                                                type="number"
                                                value={product.price}
                                                onChange={(e) => handleInputChange(index, 'price', parseFloat(e.target.value))}
                                            />

                                            <label>Slika</label>
                                            <input
                                                type="file"
                                                accept="image/*"
                                                onChange={(e) => handleImageUpload(e, index)}
                                                style={{ marginBottom: "4px" }}
                                            />
                                            <div className="div-block-23">
                                                {product.imageUrl && (
                                                    <>
                                                        <img src={product.imageUrl} alt="Slika proizvoda" style={{ width: '100px' }} />
                                                        <button
                                                            type="button"
                                                            className="button-7 btn2"
                                                            onClick={() => handleDeleteImage(index)}
                                                            style={{ color: "white" }}
                                                        >
                                                            Obriši sliku
                                                        </button>
                                                    </>
                                                )}
                                            </div>

                                            <label>Odaberi podkategorije</label>
                                            <div className="div-block-2">
                                                {product.allSubCategories.map((subcategory) => {
                                                    const isChecked = product.selectedSubCategories.some(selected => selected.id === subcategory.id);

                                                    return (
                                                        <label key={subcategory.id} className="w-checkbox">
                                                            <input
                                                                type="checkbox"
                                                                className="w-checkbox-input"
                                                                checked={isChecked}
                                                                onChange={(e) => handleSubcategoryChange(index, subcategory, e.target.checked)}
                                                            />
                                                            <span className="w-form-label">{subcategory.name}</span>
                                                        </label>
                                                    );
                                                })}
                                            </div>

                                            <div className="div-block-19">
                                                <input
                                                    type="button"
                                                    className="submit-button btn2 w-button"
                                                    value="Sačuvaj promene"
                                                    onClick={() => handleSave(product)}
                                                />
                                                <a
                                                    className="button-remove btn2 w-button"
                                                    onClick={() => handleDelete(product.id)}
                                                >
                                                    Obriši
                                                </a>
                                            </div>
                                        </form>
                                    </div>
                                ))}

                        </div>
                    </div>
                </div>
            </div>
        </section>

    )

}

export default ProductSettings;