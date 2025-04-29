import React, { useEffect } from "react";

interface NotificationProps {
    title: string;
    text: string;
    onClose: () => void;
}

const Notification: React.FC<NotificationProps> = ({ title, text, onClose }) => {

    useEffect(() => {
        const timer = setTimeout(() => {
            onClose();
        }, 8000);

        return () => clearTimeout(timer);
    }, [onClose]);

    return (
        <div className="notification-div">
            <div className="notification-text-div">
                <div className="notification-title">{title}</div>
                <div className="notification-text">
                    {text.split('\n').map((line, index) => (
                        <React.Fragment key={index}>
                            {line}
                            <br />
                        </React.Fragment>
                    ))}
                </div>
            </div>
            <div className="notification-close-div">
                <a
                    href="#"
                    className="notification-close w-inline-block"
                    onClick={(e) => {
                        e.preventDefault();
                        onClose();
                    }}
                >
                    <img
                        loading="lazy"
                        src="https://cdn.prod.website-files.com/680625384249960b2e79d540/6806255ff8e01112109d73e6_x-02%20(2).svg"
                        alt="Zatvori notifikaciju"
                        className="image-6"
                    />
                </a>
            </div>
        </div>
    );
};

export default Notification;
