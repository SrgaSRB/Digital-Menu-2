import React, { useEffect, useState } from "react";
import api from "../../services/api";
import Loader from "../shared/Loader";

interface Category {
    id: string;
    name: string;
    allSubCategoryLists: SubcategoryTemp[];
    selectedSubCategoryLists: SubcategoryTemp[];
}

interface SubcategoryTemp {
    id: string;
    name: string;
}

interface Subcategory {
    id: string;
    name: string;
    description: string;
}


const CategoriesSettings: React.FC = () => {

    const [categories, setCategories] = useState<Category[]>([]);
    const [newCategoryName, setNewCategoryName] = useState("");
    const [categoryNames, setCategoryNames] = useState<{ [key: string]: string }>({});

    const [subCategories, setSubCategories] = useState<Subcategory[]>([]);
    const [loading, setLoading] = useState(true);

    const [newSubcategoryName, setNewSubcategoryName] = useState("");
    const [newSubcategoryDescription, setNewSubcategoryDescription] = useState("");
    const [selectedCategoryId, setSelectedCategoryId] = useState<string>("");


    useEffect(() => {
        fetchCategories();
        fetchSubCategories();
    }, []);

    const fetchSubCategories = async () => {

        setLoading(true);

        const user = localStorage.getItem("user");
        if (!user) return;

        const parsedUser = JSON.parse(user);

        try {
            const response = await api.get(`/categories/admin/subcategories/${parsedUser.localId}`);
            setSubCategories(response.data);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }

    }

    const fetchCategories = async () => {

        setLoading(true);

        const user = localStorage.getItem("user");
        if (!user) return;

        const parsedUser = JSON.parse(user);

        try {
            const response = await api.get(`/categories/by-local/${parsedUser.localId}`);
            setCategories(response.data);

            const namesMap: { [key: string]: string } = {};
            response.data.forEach((cat: Category) => {
                namesMap[cat.id] = cat.name;
            });
            setCategoryNames(namesMap);

        } catch (error) {
            console.error("Greška pri učitavanju kategorija:", error);
        } finally {
            setLoading(false);
        }
    };


    const handleCreateCategory = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const user = localStorage.getItem("user");
        if (!user) return;

        const parsedUser = JSON.parse(user);

        try {
            await api.post("/categories", {
                name: newCategoryName,
                localId: parsedUser.localId
            });

            alert("Kategorija uspešno dodata!");
            setNewCategoryName("");

            await fetchCategories();

        } catch (error) {
            console.error("Greška pri kreiranju kategorije:", error);
            alert("Greška pri kreiranju kategorije!");
        }
    };

    const handleUpdateCategory = async (categoryId: string, newName: string) => {
        const category = categories.find(c => c.id === categoryId);
        if (!category) return;

        try {
            await api.put(`/categories/${categoryId}`, {
                name: newName,
                selectedSubCategoryIds: category.selectedSubCategoryLists.map(sub => sub.id)
            });

            alert("Kategorija uspešno izmenjena!");
            await fetchCategories();
        } catch (error) {
            console.error("Greška pri izmeni kategorije:", error);
            alert("Greška pri izmeni kategorije!");
        }
    };


    const handleDeleteCategory = async (id: string) => {
        if (!window.confirm("Da li ste sigurni da želite da obrišete kategoriju?")) return;

        try {
            await api.delete(`/categories/${id}`);
            alert("Kategorija uspešno obrisana!");

            await fetchCategories();

        } catch (error) {
            console.error("Greška pri brisanju kategorije:", error);
            alert("Greška pri brisanju kategorije!");
        }
    };

    const handleSubcategoryToggle = (categoryId: string, subcategoryId: string, isChecked: boolean) => {
        setCategories(prevCategories => prevCategories.map(cat => {
            if (cat.id !== categoryId) return cat;

            let updatedSelected = [...cat.selectedSubCategoryLists];

            if (isChecked) {
                if (!updatedSelected.some(sub => sub.id === subcategoryId)) {
                    const subcategoryToAdd = cat.allSubCategoryLists.find(sub => sub.id === subcategoryId);
                    if (subcategoryToAdd) {
                        updatedSelected.push(subcategoryToAdd);
                    }
                }
            } else {
                updatedSelected = updatedSelected.filter(sub => sub.id !== subcategoryId);
            }

            return {
                ...cat,
                selectedSubCategoryLists: updatedSelected
            };
        }));
    };

    const handleInputChangeSubcategory = (index: number, field: keyof Subcategory, value: any) => {
        const updated = [...subCategories];
        (updated[index] as any)[field] = value;
        setSubCategories(updated);
    };

    const handleSaveSubcategory = async (subcategory: Subcategory) => {
        try {
            await api.put(`/subcategories/${subcategory.id}`, {
                name: subcategory.name,
                description: subcategory.description
            });

            alert("Subkategorija uspešno sačuvana!");
            await fetchSubCategories();
        } catch (error) {
            console.error("Greška pri čuvanju subkategorije:", error);
            alert("Greška pri čuvanju!");
        }
    };

    const handleDeleteSubcategory = async (id: string) => {
        if (!window.confirm("Da li želite da obrišete potkategoriju?")) return;

        try {
            await api.delete(`/categories/admin/subcategories/${id}`);
            alert("Subkategorija uspešno obrisana!");
            await fetchSubCategories();
        } catch (error) {
            console.error("Greška pri brisanju:", error);
            alert("Greška pri brisanju!");
        }
    };

    const handleSubcategory = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setLoading(true);
    
        try {
            if (!selectedCategoryId) {
                alert("Morate izabrati kategoriju za novu potkategoriju.");
                setLoading(false);
                return;
            }
    
            await api.post(`/categories/admin/subcategories`, {
                name: newSubcategoryName,
                description: newSubcategoryDescription,
                categoryId: selectedCategoryId
            });
    
            alert("Potkategorija uspešno dodata!");
            setNewSubcategoryName("");
            setNewSubcategoryDescription("");
            setSelectedCategoryId("");
            await fetchSubCategories(); 
    
        } catch (error) {
            console.error("Greška pri kreiranju potkategorije:", error);
            alert("Greška pri kreiranju!");
        } finally {
            setLoading(false);
        }
    };
    


    <Loader fullScreen size="60px" color="#222" />

    return (
        <section className="categories-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="categories-wrapper">
                    <div className="div-block-11">
                        <div className="form-block-6 w-form">
                            <div className="text-block-11">Dodaj kategoriju</div>
                            <form id="email-form" name="email-form" onSubmit={handleCreateCategory}>
                                <label htmlFor="name-5">Naziv*</label>
                                <input className="w-input" maxLength={256} name="name-5" data-name="Name 5" placeholder="Unesi naziv kategorije" type="text" id="name-5"
                                    value={newCategoryName}
                                    onChange={(e) => setNewCategoryName(e.target.value)}
                                />
                                <input type="submit" data-wait="Please wait..." className="w-button" value="Napravi kategoriju" />
                            </form>
                        </div>
                        <div className="form-block-3 w-form">
                            <div className="text-block-12">Dodaj potkategoriju</div>
                            <form id="email-form-2" onSubmit={handleSubcategory}>
                                
                                <label htmlFor="name-5">Naziv*</label>
                                <input className="w-input" maxLength={256} name="name-6" data-name="Name 6" placeholder="Unesi naziv potkategorija " type="text" id="name-6"
                                    value={newSubcategoryName}
                                    onChange={e => setNewSubcategoryName(e.target.value)}
                                />
                                
                                <label htmlFor="">Opis</label>
                                <input className="w-input" maxLength={256} name="field-10" data-name="Field 10" placeholder="Dodaj opis (Npr za potkategoriju doručak: &quot;Doručak služimo svakog dana od 8:00h - 11:00h&quot;)" type="text"
                                    value={newSubcategoryDescription}
                                    onChange={e => setNewSubcategoryDescription(e.target.value)}
                                />
                                
                                <label>Izaberi kategoriju*</label>
                                <select
                                    className="w-input"
                                    value={selectedCategoryId}
                                    onChange={(e) => setSelectedCategoryId(e.target.value)}
                                >
                                    <option value="">-- Izaberite kategoriju --</option>
                                    {categories.map((cat) => (
                                        <option key={cat.id} value={cat.id}>
                                            {cat.name}
                                        </option>
                                    ))}
                                </select>

                                <input type="submit" data-wait="Please wait..." className="w-button" value="Napravi potkategorija " />
                            </form>
                        </div>
                    </div>
                    <div className="div-block-12">
                        <div className="div-block-13">
                            <div className="text-block-9">Lista kategorija</div>
                            <div className="categories-list">

                                {categories.map((category) => (
                                    <div className="form-block-4 w-form" key={category.id}>
                                        <form
                                            id="email-form-3"
                                            name="email-form-3"
                                            data-name="Email Form 3"
                                            onSubmit={(e) => {
                                                e.preventDefault();
                                                handleUpdateCategory(category.id, categoryNames[category.id]);
                                            }}
                                        >
                                            <label>Naziv*</label>
                                            <input
                                                className="text-field-6 w-input"
                                                maxLength={256}
                                                name="name-7"
                                                placeholder=""
                                                type="text"
                                                id={`name-7-${category.id}`}
                                                value={categoryNames[category.id] || ""}
                                                onChange={(e) => {
                                                    setCategoryNames(prev => ({
                                                        ...prev,
                                                        [category.id]: e.target.value
                                                    }));
                                                }}
                                            />

                                            <label>Lista podkategorija u kategoriji</label>
                                            <div className="div-block-14">
                                                {category.allSubCategoryLists.map((sub) => (
                                                    <label key={sub.id} className="w-checkbox">
                                                        <input
                                                            type="checkbox"
                                                            className="w-checkbox-input"
                                                            checked={category.selectedSubCategoryLists.some(s => s.id === sub.id)}
                                                            onChange={(e) => handleSubcategoryToggle(category.id, sub.id, e.target.checked)}
                                                        />
                                                        <span className="w-form-label">{sub.name}</span>
                                                    </label>
                                                ))}

                                            </div>
                                            <div className="div-block-15">
                                                <input type="submit" data-wait="Please wait..." className="w-button" value="Sačuvaj izmene" />
                                                <a className="button-remove w-button" onClick={() => handleDeleteCategory(category.id)}>Obriši</a>
                                            </div>
                                        </form>
                                    </div>
                                ))}


                            </div>
                        </div>
                        <div className="div-block-13">
                            <div className="text-block-10">Lista potkategorija</div>
                            <div className="subcategories-list">

                                {subCategories.map((subcategory, index) => (
                                    <div className="form-block-5 w-form" key={subcategory.id}>
                                        <form
                                            id="email-form-4"
                                            name="email-form-4"
                                            onSubmit={(e) => {
                                                e.preventDefault();
                                                handleSaveSubcategory(subcategory);
                                            }}
                                        >
                                            <label>Naziv*</label>
                                            <input
                                                className="text-field-6 w-input"
                                                maxLength={256}
                                                name="name-8"
                                                placeholder=""
                                                type="text"
                                                id={`name-8-${subcategory.id}`}
                                                value={subcategory.name}
                                                onChange={(e) => handleInputChangeSubcategory(index, "name", e.target.value)}
                                            />

                                            <label>Opis</label>
                                            <input
                                                className="w-input"
                                                maxLength={256}
                                                name="field-10"
                                                placeholder="Dodaj opis..."
                                                type="text"
                                                value={subcategory.description}
                                                onChange={(e) => handleInputChangeSubcategory(index, "description", e.target.value)}
                                            />

                                            <div className="div-block-16">
                                                <input
                                                    type="submit"
                                                    data-wait="Please wait..."
                                                    className="submit-button-2 w-button"
                                                    value="Sačuvaj izmene"
                                                />
                                                <a
                                                    className="button-remove w-button"
                                                    onClick={() => handleDeleteSubcategory(subcategory.id)}
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
            </div>
        </section>

    );
}

export default CategoriesSettings;