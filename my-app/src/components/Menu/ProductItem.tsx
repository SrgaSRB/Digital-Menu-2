import React, { useState } from 'react';

interface ProductItemProps {
    product: {
        id: string;
        imageUrl: string;
        haveImage: boolean;
        name: string;
        description: string;
        additionalDescription: string;
        price: number;
    };
}

const ProductItem: React.FC<ProductItemProps> = ({ product }) => {

    const [fullScreenImage, setFullScreenImage] = useState<string | null>(null);

    const handleImageClick = (url: string) => {
        console.log("test")
        setFullScreenImage(url);
    };

    const handleCloseImage = () => {
        setFullScreenImage(null);
    };

    return (
        <>
            {fullScreenImage && (
                <div className="full-screen-photo-cover" onClick={handleCloseImage}>
                    <div className="full-screen-photo-div">
                        <img src={fullScreenImage} alt="Fullscreen" className="image-13" />
                    </div>
                </div>
            )}

            <div className="entity-type-3" key={product.id}>
                {product.haveImage && (
                    <div className="entity-type-3-image-div" onClick={() => handleImageClick(product.imageUrl)}>
                        <img
                            onClick={() => handleImageClick(product.imageUrl)}
                            src={product.imageUrl}
                            loading="lazy"
                            sizes="(max-width: 767px) 100vw, (max-width: 991px) 728px, 940px"
                            alt={product.name}
                            className="entity-type-2-image"
                        />
                    </div>
                )}
                <div className="entity-type-3-info">
                    <div className="entity-type-3-info-name">{product.name}</div>
                    <div className="entity-type-3-info-description">{product.description}</div>
                    <div className="entity-type-3-info-description">{product.additionalDescription}</div>
                    <div className="entity-type-3-info-price">
                        <span className="text-span-12">{product.price.toFixed(2)}</span> rsd
                    </div>
                </div>
            </div>
        </>
    );
};

export default ProductItem;