import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';

import Header from '../components/Menu/Header';
import Categories from '../components/Menu/Categories';
import ProductList from '../components/Menu/ProductList';
import Notification from '../components/Menu/Notification';
import api from '../services/api';

interface Category {
  id: string;
  name: string;
  description: string;
}


interface NotificationModel {
  id: string;
  title: string;
  text: string;
}

const MenuPage: React.FC = () => {
  const { localId } = useParams();
  const [selectedSubcategory, setSelectedSubcategory] = useState<Category | null>(null);
  const [notifications, setNotifications] = useState<NotificationModel[]>([]);

  useEffect(() => {
    const fetchNotifications = async () => {
      try {
        const response = await api.get(`/notification/by-local/${localId}`);
        console.log("Notifikacije: ", response.data);
        setNotifications(response.data);
      } catch (error) {
        console.error("Error fetching notifications:", error);
      }
    };

    if (localId) {
      fetchNotifications();
    }
  }, [localId]);

  const handleCloseNotification = (id: string) => {
    setNotifications((prev) => prev.filter((n) => n.id !== id));
  };

  if (!localId) {
    return <div>Greška: Lokal nije pronađen.</div>;
  }

  return (
    <div className="body">
      <Header localId={localId} />


      <section className='hero-section'>
        <div className="w-layout-blockcontainer container w-container">
          <div className='hero-section-wrapper'>
            <div className='food-bar-and-entities-list-div'>

              <Categories
                localId={localId}
                onSelectSubcategory={(subcategory) => setSelectedSubcategory(subcategory)}
              />

              {selectedSubcategory ? (
                <ProductList
                  localId={localId}
                  subcategoryId={selectedSubcategory.id}
                  subcategoryName={selectedSubcategory.name}
                  subcategoryDescription={selectedSubcategory.description}
                />
              ) : (
                <div >Izaberi kategoriju</div>
              )}

            </div>
          </div>
        </div>
      </section >

      {notifications.length > 0 && (
        <section className="notification-section">
          <div className="w-layout-blockcontainer container w-container">
            {notifications.map((notification) => (
              <Notification
                key={notification.id}
                title={notification.title}
                text={notification.text}
                onClose={() => handleCloseNotification(notification.id)}
              />
            ))}
          </div>
        </section>
      )}

    </div >
  );
};

export default MenuPage;
