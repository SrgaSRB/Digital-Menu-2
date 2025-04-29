import React, { useEffect, useState } from "react";
import api from "../../services/api";

interface Notificatoin {
    id: string;
    title: string;
    text: string;
}

const NotificationSettings: React.FC = () => {

    const [notificationTitle, setNotificationTitle] = useState("");
    const [notidicationText, setNotificationText] = useState("");
    const [loading, setLoading] = useState(true);
    const [notifications, setNotifications] = useState<Notificatoin[]>([]);

    useEffect(() => {
        fetchNotifications();
    }, []);

    const fetchNotifications = async () => {

        setLoading(true);

        const user = localStorage.getItem("user");
        if (!user) return;

        const parsedUser = JSON.parse(user);

        try {
            const response = await api.get(`/notification/${parsedUser.localId}`)
            setNotifications(response.data);
            console.log(response.data)
        } catch (error) {
            alert("Greska pri ucitavanju notifikacije")
            console.error(error)
        } finally {
            setLoading(false)
        }

    }

    const handleCreateNotification = async (e: React.FormEvent<HTMLFormElement>) => {

        setLoading(true);

        const user = localStorage.getItem("user");
        if (!user) return;

        const parsedUser = JSON.parse(user);

        try {
            const response = await api.post(`/notification/${parsedUser.localId}`, {
                title: notificationTitle,
                text: notidicationText
            });
            alert("Notifikacija uspesno dodata")
            console.log("parsedUser:", parsedUser);
            console.log("parsedUser.localId:", parsedUser.localId);
            fetchNotifications();
        } catch (error) {
            alert("Greska pri dodavanju notifikacije")
            console.error(error)
        } finally {
            setLoading(false)
        }

    }

    const handleUpdateNotification = async (notification: Notificatoin) => {
        setLoading(true);

        try {
            await api.put(`/notification/${notification.id}`, {
                title: notification.title,
                text: notification.text,
            });

            alert("Notifikacija uspešno ažurirana!");
            await fetchNotifications();
        } catch (error) {
            alert("Greška pri ažuriranju notifikacije!");
            console.error(error);
        } finally {
            setLoading(false);
        }
    };


    const handleNotificationChange = (index: number, field: keyof Notificatoin, value: any) => {
        const updated = [...notifications];
        (updated[index] as any)[field] = value;
        setNotifications(updated);
    };

    const handleDeleteNotification = async (id: string) => {
        if (!window.confirm("Da li ste sigurni da želite da obrišete ovu notifikaciju?")) return;

        try {
            await api.delete(`/notification/${id}`);
            alert("Notifikacija uspešno obrisana!");
            await fetchNotifications();
        } catch (error) {
            alert("Greška pri brisanju notifikacije!");
            console.error(error);
        }
    };



    return (
        <section className="admin-notification-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="admin-notification-wrapper">
                    <div className="div-block-20">
                        <div className="w-form">
                            <div className="text-block-14">Dodaj notifikaciju</div>
                            <form id="email-form-5" name="email-form-5" data-name="Email Form 5" onSubmit={handleCreateNotification}>
                                <label htmlFor="name-9">Naslov*</label>
                                <input className="text-field-7 w-input" maxLength={256} name="name-9" data-name="Name 9" placeholder="Unesi naslov notifikacije" type="text" required
                                    value={notificationTitle}
                                    onChange={e => setNotificationTitle(e.target.value)}
                                />
                                <label htmlFor="field-8">Tekst*</label>
                                <textarea id="field-8" name="field-8" maxLength={500} data-name="Field 8" placeholder="Unesi opis notifikacije" required className="w-input"
                                    value={notidicationText}
                                    onChange={e => setNotificationText(e.target.value)}
                                />
                                <input type="submit" data-wait="Please wait..." className="submit-button-4 w-button" value="Kreiraj notifikaciju" />
                            </form>
                        </div>
                    </div>
                    <div className="div-block-21">
                        <div className="text-block-13">Lista notifikacija</div>
                        <div className="admin-notification-list">

                            {notifications.map((notification, index) => (
                                <div className="form-block-7 w-form" key={notification.id}>
                                    <form
                                        id="email-form-6"
                                        name="email-form-6"
                                        onSubmit={(e) => {
                                            e.preventDefault();
                                            handleUpdateNotification(notification);
                                        }}
                                        className="form-3"
                                    >
                                        <label>Naslov*</label>
                                        <input
                                            className="w-input"
                                            maxLength={256}
                                            name="name-10"
                                            type="text"
                                            required
                                            value={notification.title}
                                            onChange={(e) => handleNotificationChange(index, "title", e.target.value)}
                                        />

                                        <label>Tekst*</label>
                                        <input
                                            className="w-input"
                                            maxLength={500}
                                            name="email-4"
                                            type="text"
                                            required
                                            value={notification.text}
                                            onChange={(e) => handleNotificationChange(index, "text", e.target.value)}
                                        />

                                        <div className="div-block-22">
                                            <input type="submit" className="submit-button-3 w-button" value="Sačuvaj izmene" />
                                            <a className="button-remove w-button" onClick={() => handleDeleteNotification(notification.id)}>
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

export default NotificationSettings;