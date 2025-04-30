import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";

import Navbar from "../components/Admin/Navbar";
import ProductSettings from "../components/Admin/ProductSettings";
import CategoriesSettings from "../components/Admin/CategorySettings";
import NotificationSettings from "../components/Admin/NotificationSettings";
import ThemeSelectorSettings from "../components/Admin/ThemeSelector";
import Dashboard from "../components/Admin/Dashboard";

const AdminPanel: React.FC = () => {
    return (
        <div className="body-3">
            <Navbar />
            <Routes>
                <Route path="/" element={<ProductSettings />} />
                <Route path="lokal" element={<Dashboard />} />
                <Route path="proizvodi" element={<ProductSettings />} />
                <Route path="kategorije" element={<CategoriesSettings />} />
                <Route path="notifikacije" element={<NotificationSettings />} />
                {/*
                <Route path="tema" element={<ThemeSelectorSettings />} />
                */}
            </Routes>
        </div>
    );
};

export default AdminPanel;
