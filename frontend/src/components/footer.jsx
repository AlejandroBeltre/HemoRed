import React, { Component } from 'react'
import { XOutlined, InstagramFilled, FacebookFilled } from '@ant-design/icons'
import './footer.css'
import logo from '../assets/images/HemoRedLogo.png'
import { Link } from 'react-router-dom';

function Footer() {
    return (
        <footer>
            <div className="container-footer">
                <div className="footer-content">
                    <div className="footer-links">
                        <img src={logo} alt="Logo" className='logo' />
                        <ul className='footer-list'>
                            <li>DONAR</li>
                            <li><Link to="/requestBlood" style={{textDecoration: 'none', color: 'inherit'}}>SOLICITAR SANGRE</Link></li>
                            <li>CAPACITACIONES</li>
                        </ul>
                    </div>
                    <div className="social-icons">
                        <InstagramFilled />
                        <FacebookFilled />
                        <XOutlined />
                    </div>
                </div>
                <div className="footer-bottom-foot">
                    <p>Grupo 02 | 2024 | Aseguramiento de la calidad</p>
                </div>
            </div>
        </footer>
    )
}

export default Footer