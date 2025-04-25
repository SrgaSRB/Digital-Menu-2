import React from "react";

const Dashboard: React.FC = () => {

    return (
        <section className="dashboard-section">
            <div className="w-layout-blockcontainer container-2 w-container">
                <div className="dashboard-wrapper">
                    <div className="text-block-11">Dashboard</div>
                    <div className="text-block-12">Statistika</div>
                    <div className="text-block-13">Kategorije</div>
                    <div className="text-block-14">Potkategorije</div>
                    <div className="text-block-15">Jela</div>
                    <div className="text-block-16">Porudžbine</div>
                </div>
            </div>
        </section>
    );
}

export default Dashboard;