// src/components/Menu/Categories.tsx
import React, { useEffect, useState } from 'react';
import axios from '../../services/api';
import Loader from '../shared/Loader';
import api from '../../services/api';

interface Subcategory {
  id: string;
  name: string;
  description: string;
}

interface Category {
  id: string;
  name: string;
  description: string;
  subCategories: Subcategory[];
}

interface Props {
  localId: string;
  onSelectSubcategory: (subcategory: Subcategory) => void;
}

const Categories: React.FC<Props> = ({ localId, onSelectSubcategory }) => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedMain, setSelectedMain] = useState<Category | null>(null);
  const [selectedSub, setSelectedSub] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCategories = async () => {
      setLoading(true);
      try {
        const response = await api.get(`/categories/${localId}`);
        setCategories(response.data);
      } catch (error) {
        console.error("Error fetching categories:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchCategories();
  }, [localId]);

  const handleMainSelect = (cat: Category) => {
    setSelectedMain(cat);
    setSelectedSub(null);
  };

  const handleSubSelect = (sub: Subcategory) => {
    setSelectedSub(sub.id);
    onSelectSubcategory(sub);
  };

  if (loading) {
    return <Loader fullScreen size="60px" color="#222" />;
  }

  return (
    <>
      {/* Main categories */}
      <div className="foodbar-type-1">
        {categories.map(cat => (
          <div
            key={cat.id}
            className={`foodbar-type-1-category ${
              selectedMain?.id === cat.id ? 'foodbar-type-1-category-selected' : ''
            }`}
            onClick={() => handleMainSelect(cat)}
          >
            <div className="foodbar-type-1-category-icon">
              <img
                src="/icons/default-main.png"
                alt={cat.name}
                className="foodbar-type-1-category-icon-image"
              />
            </div>
            <div className="foodbar-type-1-category-name">{cat.name}</div>
          </div>
        ))}
      </div>

      {/* Subcategories */}
      {selectedMain && (
        <div className="foodbar-type-1 bottom-footbar">
          {selectedMain.subCategories.map(sub => (
            <div
              key={sub.id}
              className={`foodbar-type-1-category ${
                selectedSub === sub.id ? 'foodbar-type-1-category-selected' : ''
              }`}
              onClick={() => handleSubSelect(sub)}
            >
              <div className="foodbar-type-1-category-icon">
                <img
                  src="/icons/default-sub.png"
                  alt={sub.name}
                  className="foodbar-type-1-category-icon-image"
                />
              </div>
              <div className="foodbar-type-1-category-name">{sub.name}</div>
            </div>
          ))}
        </div>
      )}
    </>
  );
};

export default Categories;