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
                            <li><Link to="/scheduleAppointment" style={{textDecoration: 'none', color: 'inherit'}} onClick={() => window.scrollTo(0, 0)}>DONAR</Link></li>
                            <li><Link to="/requestBlood" style={{textDecoration: 'none', color: 'inherit'}} onClick={() => window.scrollTo(0, 0)}>SOLICITAR SANGRE</Link></li>
                            <li><Link to="/campaigns" style={{textDecoration: 'none', color: 'inherit'}} onClick={() => window.scrollTo(0, 0)}>CAMPAÑAS</Link></li>
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