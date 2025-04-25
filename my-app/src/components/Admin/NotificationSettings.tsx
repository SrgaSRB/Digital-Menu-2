import React from "react";

const NotificationSettings: React.FC = () => {

    return (
        <section className="admin-notification-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="admin-notification-wrapper">
                    <div className="div-block-20">
                        <div className="w-form">
                            <div className="text-block-14">Dodaj notifikaciju</div>
                            <form id="email-form-5" name="email-form-5" data-name="Email Form 5" method="get" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="0d2ed32e-3fe0-0c96-1806-982811242962" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                <label htmlFor="name-9">Naslov*</label>
                                <input className="text-field-7 w-input" maxLength={256} name="name-9" data-name="Name 9" placeholder="Unesi naslov notifikacije" type="text" id="name-9" required />
                                <label htmlFor="field-8">Opis*</label>
                                <textarea id="field-8" name="field-8" maxLength={500} data-name="Field 8" placeholder="Unesi opis notifikacije" required className="w-input"></textarea>
                                <input type="submit" data-wait="Please wait..." className="submit-button-4 w-button" value="Kreiraj notifikaciju" />
                            </form>
                        </div>
                    </div>
                    <div className="div-block-21">
                        <div className="text-block-13">Lista notifikacija</div>
                        <div className="admin-notification-list">
                            <div className="form-block-7 w-form">
                                <form id="email-form-6" name="email-form-6" data-name="Email Form 6" method="get" className="form-3" data-wf-page-id="6806d17ca79e347fe1b77f0c" data-wf-element-id="0d2ed32e-3fe0-0c96-1806-982811242975" data-turnstile-sitekey="0x4AAAAAAAQTptj2So4dx43e">
                                    <label htmlFor="name-9">Naslov*</label>
                                    <input className="w-input" maxLength={256} name="name-10" data-name="Name 10" placeholder="" type="text" id="name-10" required />
                                    <label htmlFor="email-4">Opis*</label>
                                    <input className="w-input" maxLength={256} name="email-4" data-name="Email 4" placeholder="" type="email" id="email-4" required />
                                    <div className="div-block-22">
                                        <input type="submit" data-wait="Please wait..." className="submit-button-3 w-button" value="Sačuvaj izmene" />
                                        <a className="button-remove w-button">Obriši</a>
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

export default NotificationSettings;