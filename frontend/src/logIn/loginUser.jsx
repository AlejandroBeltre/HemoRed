import React, { Component, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import Footer from '../components/footer'
import Headers from '../components/header'
import { ArrowLeftOutlined, EyeOutlined, EyeInvisibleOutlined} from '@ant-design/icons'
import loginPhoto from '../assets/images/loginPhoto.png'
import './loginUser.css'

function LoginUser() {
    const [passwordVisible, setPasswordVisible] = useState(false);
    const [formData, setFormData] = useState({
        email: '',
        password: ''
    })
    const handleChange = (e) => {
        const { name, value } = e.target;
            setFormData({
                ...formData,
                [name]: value
            });
      };
    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(formData);
        navigate('/');
    }
    return (
      <div>
        <Headers />
        <div className="login-container">
            <div className="login-form">
                <ArrowLeftOutlined className="back" onClick={handleBack}/>
                <h1>¡Hola de nuevo!</h1>
                <p>Tu apoyo continuo es esencial: sigue donando y juntos construiremos un mundo mejor.</p>
                <form>
                <div className="form-group-login">
                    <label htmlFor="email">Correo electrónico</label>
                    <input 
                    type="email" 
                    id="email" 
                    name="email" 
                    value={formData.email}
                    onChange={handleChange}
                    required />
                </div>
                <div className="form-group-login">
                <label htmlFor="password">Contraseña:</label>
                <div className="password-input">
                    <input
                    type={passwordVisible ? 'text' : 'password'}
                    id="password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                    />
                <button type="button" onClick={() => setPasswordVisible(!passwordVisible)} className='toggle-password'>
                  {passwordVisible ? <EyeInvisibleOutlined /> : <EyeOutlined />}
                </button>
                </div>
                <p className='recover-account'><Link to="/recoverPassword" style={{color: 'inherit'}}>Recuperar Contraseña</Link></p>
            </div>
                <button type="submit" className="submit-button">Iniciar sesión</button>
                <p className='no-account'><Link to="/registerUser" style={{color: 'inherit'}}>No tengo cuenta</Link></p>
                </form>
            </div>
            <div className="image-container">
                <img src={loginPhoto} alt="Office worker at desk" className='image'/>
            </div>
            </div>
        <Footer />
      </div>
    )
  }


export default LoginUser;