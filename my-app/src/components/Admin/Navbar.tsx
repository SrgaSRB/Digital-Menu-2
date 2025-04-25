import React from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";

const Navbar: React.FC = () => {
    const location = useLocation();
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        navigate("/login");
    };

    return (
        <div className="admin-navbar">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="admin-wrapper">
                    <div className="div-block-17">
                        <Link
                            to="/admin/proizvodi"
                            className={`link-block ${location.pathname === "/admin/proizvodi" ? "selected-lb" : ""} w-inline-block`}
                        >
                            <div>Proizvodi</div>
                        </Link>
                        <Link
                            to="/admin/kategorije"
                            className={`link-block ${location.pathname === "/admin/kategorije" ? "selected-lb" : ""} w-inline-block`}
                        >
                            <div>Kategorije</div>
                        </Link>
                        <Link
                            to="/admin/notifikacije"
                            className={`link-block ${location.pathname === "/admin/notifikacije" ? "selected-lb" : ""} w-inline-block`}
                        >
                            <div>Notifikacije</div>
                        </Link>
                    </div>
                    <div className="div-block-18" onClick={handleLogout}>
                        <Link to="/admin/logout" className="link-block logout w-inline-block">
                            <img
                                loading="lazy"
                                src="https://cdn.prod.website-files.com/680625384249960b2e79d540/6806d1882993b4c6c34d5cbc_exit.png"
                                alt=""
                                className="image"
                            />
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Navbar;