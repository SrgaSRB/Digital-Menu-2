import React from "react";

interface NotificationProps {
    title: string;
    text: string;
    onClose: () => void;
}

const Notification: React.FC<NotificationProps> = ({ title, text, onClose }) => {

    return (
        <section className="notification-section">
            <div className="w-layout-blockcontainer container w-container">
                <div className="notification-div">
                    <div className="notification-title">{title}</div>
                    <div className="notification-text">
                        {text}
                    </div>
                    <div className="notification-close-div">
                        <a className="notification-close w-inline-block"
                            onClick={onClose}>
                            <img loading="lazy"
                                src="https://cdn.prod.website-files.com/680625384249960b2e79d540/6806255ff8e01112109d73e5_x-02.svg"
                                alt="" className="image-6" />
                        </a>
                    </div>
                </div>
            </div>
        </section>
    );
}

export default Notification;