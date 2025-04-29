import React from 'react';

interface ProductItemProps {
    product: {
        id: string;
        imageUrl: string;
        haveImage : boolean;
        name: string;
        description: string;
        additionalDescription: string;
        price: number;
    };
}

const ProductItem: React.FC<ProductItemProps> = ({ product }) => {

    console.log("ProductItem component rendered with product:", product);

    return (
        <div className="entity-type-3" key={product.id}>
            {product.haveImage && (
                <div className="entity-type-3-image-div">
                    <img
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
    );
};

export default ProductItem;