//<Loader /> // inline div
//<Loader fullScreen size="60px" color="#222" /> // fullscreen

import React from 'react';

interface LoaderProps {
  fullScreen?: boolean;
  size?: string;        
  color?: string;       
}

const Loader: React.FC<LoaderProps> = ({ fullScreen = false, size = '40px', color = 'black' }) => {
  const style = {
    '--uib-size': size,
    '--uib-color': color,
    '--uib-speed': '0.9s',
    '--uib-stroke': '5px',
    '--mask-size': `calc(${size} / 2 - 5px)`
  } as React.CSSProperties;

  const loader = (
    <div
      style={style}
      className="loader-container"
    ></div>
  );

  if (fullScreen) {
    return <div style={overlayStyle}>{loader}</div>;
  }

  return loader;
};

const overlayStyle: React.CSSProperties = {
  position: 'fixed',
  top: 0,
  left: 0,
  width: '100vw',
  height: '100vh',
  background: 'rgba(255, 255, 255, 0.6)',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  zIndex: 9999,
};

export default Loader;

// CSS-in-JS for loader-container
const loaderStyle = `
.loader-container {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: flex-start;
  height: var(--uib-size);
  width: var(--uib-size);
  -webkit-mask: radial-gradient(circle var(--mask-size), transparent 99%, #000 100%);
  mask: radial-gradient(circle var(--mask-size), transparent 99%, #000 100%);
  background-image: conic-gradient(transparent 25%, var(--uib-color));
  animation: spin var(--uib-speed) linear infinite;
  border-radius: 50%;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}`;

const styleEl = document.createElement('style');
styleEl.textContent = loaderStyle;
document.head.appendChild(styleEl);