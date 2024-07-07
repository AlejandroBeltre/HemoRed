import React from 'react';
import './card.css';

const Card = ({ title, children, linkText, linkUrl }) => (
    <div className="card">
        <div className="card-header">
            <h2>{title}</h2>
            {linkText && linkUrl && <a href={linkUrl}>{linkText}</a>}
        </div>
        <div className="card-content">
            {children}
        </div>
    </div>
);

export default Card;