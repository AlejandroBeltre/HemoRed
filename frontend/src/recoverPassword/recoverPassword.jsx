import React, { Component, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import './recoverPassword.css'
import Headers from '../components/header'
import Footer from '../components/footer'
import { ArrowLeftOutlined } from '@ant-design/icons'

function RecoverPassword() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [notification, setNotification] = useState("");
    const [isSubmitted, setIsSubmitted] = useState(false); 
    const handleBack = () => {
        navigate(-1);
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        if (email) {
            setIsSubmitted(true);
        }
        else {
            setNotification("Por favor, ingresa tu correo electrónico.");
            setTimeout(() => setNotification(""), 2000);
            return;
        }
    };

    const handleResend = () => {
        console.log('Resend action triggered');
        setNotification("¡Reenvío exitoso!");
        setTimeout(() => setNotification(""), 2000);
      };

    if (isSubmitted === true) {
        // Step 3: Conditionally render a different part of the component after submission
        return (
            <div>
                <Headers />
                <ArrowLeftOutlined className="back" onClick={handleBack}/>
                <div className="recover-container">
                {notification && <div className="notification">{notification}</div>}
                    <h1>Recuperar Contraseña</h1>
                    <p>Revisa el correo para continuar el proceso, sino haz recibido ningún correo, solicita enviarlo de nuevo.</p>
                    <button type="submit" className="recover-button" onClick={handleResend}>Reenviar</button>
                </div>
                <Footer />
            </div>
        );
    }

    return (
      <div>
        <Headers />
        <div className="recover-container">
        <ArrowLeftOutlined className="back" onClick={handleBack}/>
        <div className="recover-form">
          <h1>Recuperar contraseña</h1>
          <p>Ingresa tu correo electrónico y te enviaremos un enlace para que puedas recuperar tu contraseña</p>
          <form onSubmit={handleSubmit}>
            {notification && <div className="notification-recover">{notification}</div>}
            <div className="form-group-recover">
              <label htmlFor="email">Correo electrónico</label>
              <input type="email" id="email" name="email" value={email} onChange={(e) => setEmail(e.target.value)}required />
            </div>
            <button type="submit" className="recover-button" onClick={handleSubmit}>Enviar</button>
          </form>
        </div>
        <Footer />
      </div>
    </div>
    )
  }

export default RecoverPassword;