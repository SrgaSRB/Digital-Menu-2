import React from "react";

const ProductSettings: React.FC = () => {

    return (
        <section className="items-section">
        <div className="w-layout-blockcontainer container-2 w-container">
            <div className="items-wrapper">
                <div className="div-block">
                    <div className="item-form create-if w-form">
                        <form id="wf-form-Create-item-form" name="wf-form-Create-item-form" data-name="Create item form" method="get" className="form create-f" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="34c0e6c9-6c20-505e-a373-be0eced873b7" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                            <div className="text-block-2">Dodaj novi proizvod</div>
                            <label htmlFor="name">Naziv*</label>
                            <input className="text-field-4 w-input" maxLength={256} name="name" data-name="Name" placeholder="Unesite naziv proizvoda" type="text" id="name" required/>
                            <label htmlFor="field">Opis*</label>
                            <textarea id="field-4" name="field-4" maxLength={256} data-name="Field 4" placeholder="Unestite opis proizvoda (npr. sastojci)" required className="textarea-4 w-input"></textarea>
                            <label htmlFor="field">Dodatni opis</label>
                            <textarea id="field-5" name="field-5" maxLength={256} data-name="Field 5" placeholder="Unesite dodatni opis proizvoda (npr. obim kod pica)" className="textarea-2 w-input"></textarea>
                            <label htmlFor="field">Cena*</label>
                            <input className="text-field-4 w-input" maxLength={256} name="field-2" data-name="Field 2" placeholder="Unesite cenu u dinarima" type="text" id="field-2" required/>
                            <div>
                                <label htmlFor="field-9">Slika</label>
                                <div className="div-block-23">
                                    <input className="text-field-8 w-input" maxLength={256} name="field-9" data-name="Field 9" placeholder="Example Text" type="text" id="field-9" required/>
                                    <a className="button-7 w-button">Obriši sliku</a>
                                </div>
                            </div>
                            <label htmlFor="">Odaberi podkategoriju</label>
                            <div className="div-block-2">
                                <label className="w-checkbox">
                                    <input type="checkbox" name="checkbox" id="checkbox" data-name="Checkbox" className="w-checkbox-input"/>
                                    <span className="w-form-label">Checkbox</span>
                                </label>

                            </div>
                            <input type="submit" data-wait="Please wait..." className="submit-button w-button" value="Napravi"/>
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
                                    <input className="text-field w-input" maxLength={256} name="name-4" data-name="Name 4" placeholder="Unesite rec u nazivu za filtriranje" type="text" id="name-4"/>
                                </div>
                                <div className="div-block-6">
                                    <label htmlFor="field" className="field-label">Opis</label>
                                    <textarea id="field-6" name="field-6" maxLength={256} data-name="Field 6" placeholder="Unesite rec u opisu za filtriranje" className="textarea-3 text-field w-input"></textarea>
                                </div>
                            </div>
                            <div className="div-block-5">
                                <div className="div-block-6">
                                    <label htmlFor="field" className="field-label">Min Cena</label>
                                    <input className="text-field w-input" maxLength={256} name="field-7" data-name="Field 7" placeholder="Unesi minimalnu cenu" type="number" id="field-7"/>
                                </div>
                                <div className="div-block-6">
                                    <label htmlFor="field" className="field-label">Max Cena</label>
                                    <input className="text-field w-input" maxLength={256} name="field-7" data-name="Field 7" placeholder="Unesi maximalnu cenu" type="number" id="field-7"/>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div className="intems-list-div">
                        <div className="item-form w-form">
                            <form id="wf-form-Create-item-form" name="wf-form-Create-item-form" data-name="Create item form" method="get" className="form" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="34c0e6c9-6c20-505e-a373-be0eced87413" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                <div className="div-block-24">
                                    <label className="w-checkbox checkbox-field">
                                        <input type="checkbox" name="Sakrij-prikaz" id="Sakrij-prikaz" data-name="Sakrij prikaz" className="w-checkbox-input checkbox"/>
                                        <span className="checkbox-label w-form-label">Sakrij proizvod</span>
                                    </label>
                                </div>
                                <label htmlFor="name">Naziv*</label>
                                <input className="text-field-5 w-input" maxLength={256} name="name-3" data-name="Name 3" placeholder="Unesite naziv proizvoda" type="text" id="name-3" required/>
                                <label htmlFor="email">Opis*</label>
                                <input className="text-field-5 w-input" maxLength={256} name="email-3" data-name="Email 3" placeholder="Unestite opis proizvoda (npr. sastojci)" type="email" id="email-3" required/>
                                <label htmlFor="field">Dodatni opis</label>
                                <input className="text-field-5 w-input" maxLength={256} name="field-3" data-name="Field 3" placeholder="Unesite dodatni opis proizvoda (npr. obim kod pica)" type="text" id="field-3"/>
                                <label htmlFor="field">Cena*</label>
                                <input className="text-field-5 w-input" maxLength={256} name="field-2" data-name="Field 2" placeholder="Unesite cenu u dinarima" type="text" id="field-2" required/>
                                <div>
                                    <label htmlFor="field-10">Slika</label>
                                    <div className="div-block-23">
                                        <input className="text-field-8 text-field-5 w-input" maxLength={256} name="field-9" data-name="Field 9" placeholder="Example Text" type="text" id="field-9" required/>
                                        <a className="button-7 btn2 w-button">Obriši sliku</a>
                                    </div>
                                </div>
                                <label htmlFor="">Odaberi podkategoriju</label>
                                <div className="div-block-2">
                                    <label className="w-checkbox">
                                        <input type="checkbox" name="checkbox-10" id="checkbox-10" data-name="Checkbox 10" className="w-checkbox-input"/>
                                        <span className="w-form-label">Checkbox</span>
                                    </label>
                                </div>
                                <div className="div-block-19">
                                    <input type="submit" data-wait="Please wait..." className="submit-button btn2 w-button" value="Sačuvaj promene"/>
                                    <a className="button-remove btn2 w-button">Obriši</a>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    )

}

export default ProductSettings;