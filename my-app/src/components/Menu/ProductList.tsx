import { useEffect, useState } from "react";
import api from "../../services/api";
import Loader from "../shared/Loader";
import ProductItem from "./ProductItem";

interface Product {
  id: string;
  name: string;
  description: string;
  additionalDescription: string;
  price: number;
  imageUrl: string;
  haveImage : boolean;
  categories: string[];
}

interface Props {
  localId?: string;
  subcategoryId: string;
  subcategoryName: string;
  subcategoryDescription?: string;
}

const ProductList: React.FC<Props> = ({ localId, subcategoryId, subcategoryName, subcategoryDescription }) => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAllProducts = async () => {
      setLoading(true);
      try {
        const response = await api.get(`/product/${localId}`);
        setProducts(response.data);
        console.log("Fetched products:", response.data);
      } catch (error) {
        console.error("Error fetching products:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchAllProducts();
  }, [localId]);

  const filteredProducts = products.filter((product) =>
    product.categories.includes(subcategoryId)
  );

  if (loading) {
    return <Loader fullScreen size="60px" color="#222" />;
  }

  return (
    
    <div className="category-entites-list">

      {subcategoryDescription && (
        <div className="subcategory-description-div" style={{ gridColumn: "span 3" }}>
          <div className="subcategory-description-image-div">
            <img
              src="https://cdn.prod.website-files.com/680625384249960b2e79d540/681134090a8e9402a04720ab_information-circle-contained.svg"
              loading="lazy"
              alt=""
              className="image-12"
            />
          </div>
          <div className="subcategory-description-text-div">
            <div className="text-block-15">
              {subcategoryDescription}
            </div>
          </div>
        </div>
      )}

      {filteredProducts.map((product) => (
        <ProductItem key={product.id} product={product} />
      ))}
    </div>
  );

};

export default ProductList;