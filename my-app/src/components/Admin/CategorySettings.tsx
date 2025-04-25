import React from "react";

const CategoriesSettings: React.FC = () => {

    return (
        <section className="categories-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="categories-wrapper">
                    <div className="div-block-11">
                        <div className="form-block-6 w-form">
                            <div className="text-block-11">Dodaj kategoriju</div>
                            <form id="email-form" name="email-form" data-name="Email Form" method="get" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="907ecb22-ed31-4fa4-a36a-201bbd5c3768" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                <label htmlFor="name-5">Naziv*</label>
                                <input className="w-input" maxLength={256} name="name-5" data-name="Name 5" placeholder="Unesi naziv kategorije" type="text" id="name-5"/>
                                <input type="submit" data-wait="Please wait..." className="w-button" value="Napravi kategoriju"/>
                            </form>
                        </div>
                        <div className="form-block-3 w-form">
                            <div className="text-block-12">Dodaj potkategoriju</div>
                            <form id="email-form-2" name="email-form-2" data-name="Email Form 2" method="get" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="907ecb22-ed31-4fa4-a36a-201bbd5c3776" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                <label htmlFor="name-5">Naziv*</label>
                                <input className="w-input" maxLength={256} name="name-6" data-name="Name 6" placeholder="Unesi naziv potkategorija " type="text" id="name-6"/>
                                <label htmlFor="">Opis</label>
                                <input className="w-input" maxLength={256} name="field-10" data-name="Field 10" placeholder="Dodaj opis (Npr za potkategoriju doručak: &quot;Doručak služimo svakog dana od 8:00h - 11:00h&quot;)" type="text" id="field-10"/>
                                <input type="submit" data-wait="Please wait..." className="w-button" value="Napravi potkategorija "/>
                            </form>
                            <div className="w-form-done">
                                <div>Thank you! Your submission has been received!</div>
                            </div>
                            <div className="w-form-fail">
                                <div>Oops! Something went wrong while submitting the form.</div>
                            </div>
                        </div>
                    </div>
                    <div className="div-block-12">
                        <div className="div-block-13">
                            <div className="text-block-9">Lista kategorija</div>
                            <div className="categories-list">
                                <div className="form-block-4 w-form">
                                    <form id="email-form-3" name="email-form-3" data-name="Email Form 3" method="get" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="907ecb22-ed31-4fa4-a36a-201bbd5c3787" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                        <label htmlFor="name-5">Naziv*</label>
                                        <input className="text-field-6 w-input" maxLength={256} name="name-7" data-name="Name 7" placeholder="" type="text" id="name-7"/>
                                        <label htmlFor="">Lista podkategorija u kategoriji</label>
                                        <div className="div-block-14">
                                            <label className="w-checkbox">
                                                <input type="checkbox" name="checkbox-11" id="checkbox-11" data-name="Checkbox 11" className="w-checkbox-input"/>
                                                <span className="w-form-label">Checkbox 11</span>
                                            </label>
                                        </div>
                                        <div className="div-block-15">
                                            <input type="submit" data-wait="Please wait..." className="w-button" value="Sačuvaj izmene"/>
                                            <a className="button-remove w-button">Obriši</a>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                        <div className="div-block-13">
                            <div className="text-block-10">Lista potkategorija</div>
                            <div className="subcategories-list">
                                <div className="form-block-5 w-form">
                                    <form id="email-form-4" name="email-form-4" data-name="Email Form 4" method="get" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="907ecb22-ed31-4fa4-a36a-201bbd5c37e1" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                        <label htmlFor="name-5">Naziv*</label>
                                        <input className="text-field-6 w-input" maxLength={256} name="name-8" data-name="Name 8" placeholder="" type="text" id="name-8"/>
                                        <label htmlFor="">Opis</label>
                                        <input className="w-input" maxLength={256} name="field-10" data-name="Field 10" placeholder="Dodaj opis (Npr za potkategoriju doručak: &quot;Doručak služimo svakog dana od 8:00h - 11:00h&quot;)" type="text" id="field-10"/>
                                        <div className="div-block-16">
                                            <input type="submit" data-wait="Please wait..." className="submit-button-2 w-button" value="Sačuvaj izmene"/>
                                            <a className="button-remove w-button">Obriši</a>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    );
}

export default CategoriesSettings;