import React, { useEffect, useState } from "react";
import api from "../../services/api";

interface Local {
    id: string;
    name: string;
    imageUrl: string;
    subscription: Date;
}

const Dashboard: React.FC = () => {

    const [local, setLocal] = useState<Local | null>(null);
    const [loading, setLoading] = useState(true);
    const [imageFile, setImageFile] = useState<File | null>(null);

    useEffect(() => {
        fetchLocal();
    }, []);

    const fetchLocal = async () => {
        setLoading(true);
        const user = localStorage.getItem("user");
        if (!user) return;

        const parsedUser = JSON.parse(user);

        try {
            const response = await api.get(`/local/${parsedUser.localId}`);
            setLocal(response.data);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    const handleSave = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!local) return;

        let uploadedImageUrl = local.imageUrl;

        if (imageFile) {
            const formData = new FormData();
            formData.append("file", imageFile);
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

        try {
            await api.put(`/local/${local.id}`, {
                name: local.name,
                imageUrl: uploadedImageUrl,
            });
            alert("Uspešno sačuvano");
            fetchLocal();
        } catch (error) {
            console.error("Greška pri čuvanju", error);
            alert("Došlo je do greške");
        }
    };

    const handleNameChange = (value: string) => {
        if (local) setLocal({ ...local, name: value });
    };

    const handleDeleteImage = () => {
        if (local) setLocal({ ...local, imageUrl: "" });
        setImageFile(null);
    };

    return (
        <section className="dashboard-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="dashboard-wrapper">
                    <div className="div-block-30">
                        <div className="div-block-27">
                            <div className="w-form">
                                <form onSubmit={handleSave} className="form-6" key={local?.id}>
                                    <label htmlFor="name-12" className="field-label-6">Ime lokala*</label>
                                    <input
                                        className="text-field-9 w-input"
                                        maxLength={256}
                                        type="text"
                                        id="name-12"
                                        required
                                        value={local?.name || ""}
                                        onChange={(e) => handleNameChange(e.target.value)}
                                    />

                                    <label htmlFor="field-11" className="field-label-7">Slika / logo lokala*</label>
                                    <input
                                        type="file"
                                        accept="image/*"
                                        onChange={(e) => setImageFile(e.target.files?.[0] || null)}
                                        style={{ fontSize: "16px", marginBottom: "10px" }}
                                    />
                                    {local?.imageUrl && (
                                        <div>
                                            <img src={local.imageUrl} alt="Logo" style={{ width: "100px", marginBottom: "8px" }} />
                                        </div>
                                    )}

                                    <a className="button-8 w-button" onClick={handleDeleteImage}>Obriši sliku</a>
                                    <input type="submit" className="w-button Submit Button 6" value="Sačuvaj izmene" />
                                </form>
                            </div>
                        </div>
                        <div className="div-block-28">
                            <div className="text-block-18">Clanarina važi do:</div>
                            <div className="text-block-17">
                                {local?.subscription && new Date(local.subscription).toLocaleDateString("sr-RS")}
                            </div>
                        </div>
                    </div>
                    <div>
                        <div className="form-block-9 w-form">
                            <form id="email-form-9" name="email-form-9" data-name="Email Form 9" method="get" className="form-5" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="1e3a3b5f-ba34-898e-8298-0c610d209e99" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                <div className="div-block-33">
                                    <div className="div-block-31">
                                        <label htmlFor="field-12" className="field-label-9">O lokalu</label>
                                        <textarea placeholder="Zanimljivosti o lokalu, kad je otvoren, ko je gazda, radno vreme..." maxLength={256} id="field-12" name="field-12" data-name="Field 12" className="textarea-5 w-input"></textarea>
                                        <label htmlFor="email-6" className="field-label-10">Kontakt</label>
                                        <div className="div-block-29">
                                            <div className="text-block-19">Telefon</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="tel" id="field-13" />
                                        </div>
                                        <div className="div-block-29">
                                            <div className="text-block-19">E-mail</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="email" id="field-13" />
                                        </div>
                                    </div>
                                    <div className="div-block-32">
                                        <label htmlFor="email-6" className="field-label-8">Drustevene mreže</label>
                                        <div className="div-block-29">
                                            <div className="text-block-19">Instagram</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="url" id="field-13" />
                                        </div>
                                        <div className="div-block-29">
                                            <div className="text-block-19">Facebook</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="url" id="field-13" />
                                        </div>
                                        <div className="div-block-29">
                                            <div className="text-block-19">LinkedIn</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="url" id="field-13" />
                                        </div>
                                        <div className="div-block-29">
                                            <div className="text-block-19">Tiktok</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="url" id="field-13" />
                                        </div>
                                        <div className="div-block-29">
                                            <div className="text-block-19">Youtube</div>
                                            <input className="text-field-11 w-input" maxLength={256} name="field-13" data-name="Field 13" placeholder="Link" type="url" id="field-13" />
                                        </div>
                                    </div>
                                </div>
                                <input type="submit" data-wait="Please wait..." className="submit-button-5 w-button" value="Sačuvaj" />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    );
}

export default Dashboard;