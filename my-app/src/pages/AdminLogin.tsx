import React, { useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";
import { JwtPayload } from "../types/jwt-payload";
import { jwtDecode } from "jwt-decode";


const AdminLogin: React.FC = () => {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");

        try {
            const response = await api.post("/auth/login", {
                username,
                password
            });

            const token = response.data.accessToken;
            localStorage.setItem("token", token);

            const decoded = jwtDecode<JwtPayload>(token);

            console.log(decoded);

            localStorage.setItem("user", JSON.stringify({
                id: decoded.UserId,
                username: decoded.Username,
                isSuperAdmin: decoded.IsGlobalAdmin === "true",
                localId: decoded.LocalId
            }));

            navigate("/admin/")

        } catch (error: any) {
            if (error.response?.status === 401) {
                setError("Pogrešno korisničko ime ili lozinka.");
            } else {
                setError("Došlo je do greške. Pokušaj ponovo.");
            }
        }
    };

    return (
        <section className="login-section">
            <div className="w-layout-blockcontainer container w-container">
                <div className="login-wrapper">
                    <div className="form-block-8 w-form">
                        <form id="email-form-7" name="email-form-7" data-name="Email Form 7" method="get" className="form-4" onSubmit={handleSubmit}>
                            <label htmlFor="name">Username</label>
                            <input className="w-input" maxLength={256} name="name-11" data-name="Name 11" placeholder="" type="text" id="name-11"
                                onChange={(e) => setUsername(e.target.value)}
                            />

                            <label htmlFor="email">Password</label>
                            <input className="w-input" maxLength={256} name="email-5" data-name="Email 5" placeholder="" type="password" id="email-5" required
                                onChange={(e) => setPassword(e.target.value)}
                            />
                            <input type="submit" data-wait="Please wait..." className="w-button" value="Login" />

                            {error && <p style={{ color: "red" }}>{error}</p>}
                        </form>
                    </div>
                </div>
            </div>
        </section>

    )
}

export default AdminLogin;