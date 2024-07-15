import React, { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Footer from '../components/footer';
import Headers from '../components/header';
import { ArrowLeftOutlined, EyeOutlined, EyeInvisibleOutlined } from '@ant-design/icons';
import loginPhoto from '../assets/images/loginPhoto.png';
import './loginUser.css';
import { UserContext } from '../UserContext';
import { loginUser, getUserById } from '../api';

function LoginUser() {
    const [passwordVisible, setPasswordVisible] = useState(false);
    const { setUser } = useContext(UserContext);
    const [formData, setFormData] = useState({
        email: '',
        password: ''
    });
    const [errors, setErrors] = useState({});
    const [notification, setNotification] = useState("");

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
    };

    const validateForm = () => {
        const newErrors = {};
        if (!formData.email) newErrors.email = 'Correo electrónico es requerido';
        if (!formData.password) newErrors.password = 'Contraseña es requerida';
        return newErrors;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formErrors = validateForm();
        if (Object.keys(formErrors).length > 0) {
            setErrors(formErrors);
        } else {
            try {
                // First, authenticate the user
                const loginResponse = await loginUser(formData);
                console.log('Login successful');
                
                // Then, fetch the full user details
                const userData = await getUserById(loginResponse.userId);
                
                // Store user data in localStorage
                localStorage.setItem('currentUser', JSON.stringify(userData));
                
                // Update UserContext
                setUser(userData);
                
                navigate('/');
            } catch (error) {
                setErrors({ email: 'Invalid email or password' });
                setNotification(error.message || "Login failed. Please try again.");
            }
        }
    };


    return (
      <div>
        <Headers />
        <div className="login-container">
            <div className="login-form">
                <ArrowLeftOutlined className="back" onClick={handleBack}/>
                <h1>¡Hola de nuevo!</h1>
                <p>Tu apoyo continuo es esencial: sigue donando y juntos construiremos un mundo mejor.</p>
                <form onSubmit={handleSubmit}>
                <div className="form-group-login">
                    <label htmlFor="email">Correo electrónico:</label>
                    <input 
                    type="email" 
                    id="email" 
                    name="email" 
                    value={formData.email}
                    onChange={handleChange}
                    required />
                    {errors.email && <span className="error">{errors.email}</span>}
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
                {errors.password && <span className="error">{errors.password}</span>}
                <p className='recover-account'><Link to="/loginUser/recoverPassword" style={{color: 'inherit'}}>Recuperar Contraseña</Link></p>
            </div>
                <button type="submit" className="submit-button">Iniciar sesión</button>
                <p className='no-account'><Link to="/registerUser" style={{color: 'inherit'}}>¿No tienes cuenta?</Link></p>
                </form>
                {notification && <div className="notification">{notification}</div>}
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