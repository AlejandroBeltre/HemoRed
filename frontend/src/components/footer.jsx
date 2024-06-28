import React, { Component } from 'react'
import { Instagram, Facebook, Twitter, MessageCircle } from 'lucide-react'
import './footer.css'

function Footer() {
    return (
        <footer>
            <div className="container">
                <div className="footer-content">
                    <div className="footer-links">
                        <h2>Herr Red</h2>
                        <ul className='footer-list'>
                            <li>DONAR</li>
                            <li>COMPRAR SANGRE</li>
                            <li>CAPACITACIONES</li>
                        </ul>
                    </div>
                    <div className="social-icons">
                        <Instagram />
                        <Facebook />
                        <Twitter />
                        <MessageCircle />
                    </div>
                </div>
                <div className="footer-bottom">
                    <p>Grupo 02 | 2024 | Aseguramiento de la calidad</p>
                </div>
            </div>
        </footer>
    )
}

export default Footer