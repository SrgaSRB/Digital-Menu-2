import React, { use, useEffect, useState } from 'react';
import api from '../../services/api';
import Loader from '../shared/Loader';


interface Local {
    name: string;
    logoUrl: string;
}

interface Props {
    localId: string;
}

const Header: React.FC<Props> = ({ localId }) => {

    const [local, setLocal] = useState<Local | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const fetchLocal = async () => {
            setLoading(true);
            try {
                const response = await api.get(`/local/header/${localId}`);
                setLocal(response.data);
            } catch (error) {
                console.error("Error fetching local data:", error);
            }
            finally {
                setLoading(false);
            }
        };

        fetchLocal();
    }, [localId]);

    if (loading) {
        return <Loader fullScreen size="60px" color="#222" />;
    }

    if (!local) {
        return <div>Error loading local header data</div>;
    }

    return (

        <div className="header-local-info">
            <div className="w-layout-blockcontainer container w-container">
                <div className="nav-wrapper">
                    <img sizes="(max-width: 645px) 100vw, 645px"
                        alt="local logo"
                        src={local.logoUrl}
                        loading="lazy" className="logo-image" />
                    <div className="local-name">{local.name}</div>
                </div>
            </div>
        </div>

    )

}

export default Header;