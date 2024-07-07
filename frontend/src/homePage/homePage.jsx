import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import '../App.css';
import './homePage.css';
import Footer from '../components/footer';
import Headers from '../components/header';
import background from '../assets/images/HomePageBackground.png';
import APositive from '../assets/images/APositive.png';
import ANegative from '../assets/images/ANegative.png';
import BPositive from '../assets/images/BPositive.png';
import BNegative from '../assets/images/BNegative.png';
import ABPositive from '../assets/images/ABPositive.png';
import ABNegative from '../assets/images/ABNegative.png';
import OPositive from '../assets/images/OPositive.png';
import ONegative from '../assets/images/ONegative.png';
import BloodDonation from '../assets/images/BloodDonation.png';
import Doctor from '../assets/images/Doctor.png';
import People from '../assets/images/People.png';
import PeopleTwo from '../assets/images/PeopleTwo.png';
import NeedBlood from '../assets/images/NeedBlood.png';

function HomePage() {
    const [isDropdownVisible, setDropdownVisible] = useState(false);
    const navigate = useNavigate();
    const handleRequestBlood = () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
        navigate('/requestBlood');
    };
    const handleDonation = () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
        navigate('/scheduleAppointment');
    };
    return (
        <div className="blood-donation-page">
            {/* Header */}
            <Headers />
            {/* Main Content */}
            <main>
                {/* Hero Section */}
                <section>
                    <img src={background} alt="Blood donation" className="hero-image" />
                </section>

                {/* Blood Types */}
                <section className="blood-types-homepage">
                    <h2>TIPOS DE SANGRE</h2>
                    <div className="blood-type-grid">
                        <img src={APositive} alt="A+" />
                        <img src={ANegative} alt="A-" />
                        <img src={BPositive} alt="B+" />
                        <img src={BNegative} alt="B-" />
                        <img src={ABPositive} alt="AB+" />
                        <img src={ABNegative} alt="AB-" />
                        <img src={OPositive} alt="O+" />
                        <img src={ONegative} alt="O-" />
                    </div>
                </section>

                {/* Donation CTA */}
                <section className="donation-cta">
                    <div className="donation-image">
                        <img src={BloodDonation} alt="Blood donation" />
                    </div>
                    <div className="donation-text">
                        <p>Con tu donación, puedes transformar el futuro de quienes más lo necesitan. ¡Haz la diferencia hoy!</p>
                        <button className="btn btn-primary" onClick={handleDonation}>Donar</button>
                    </div>
                </section>

                {/* Testimonials */}
                <section className="testimonials">
                    <h2>TESTIMONIOS</h2>
                    <div className="testimonial-grid">
                        <div className='testimonial-doctor'>
                            <img src={Doctor} alt="Doctor" />
                            <p>"Donar sangre es un acto de amor y solidaridad que puede salvar vidas. ¡Únete a la causa!"</p>
                        </div>
                        <div className='testimonial-people'>
                            <img src={People} alt="People" />
                            <p>"Gracias a la generosidad de donantes como tú, mi hijo pudo recibir la sangre que necesitaba para sobrevivir. ¡Gracias por tu ayuda!"</p>
                        </div>    
                        <div className='testimonial-people-two'>
                            <img src={PeopleTwo} alt="People" />
                            <p>"La sangre es vida, y cada donación cuenta. ¡Gracias por tu valiosa contribución!"</p>
                        </div>
                    </div>
                </section>

                {/* Need Blood Section */}
                <section className="need-blood">
                    <div className="need-blood-image">
                        <h2>¿NECESITAS SANGRE?</h2>
                        <img src={NeedBlood} alt="Need blood" />
                    </div>
                    <div className="need-blood-text">
                        <p>"Para quienes enfrentan situaciones críticas de salud, comprar sangre no es solo una necesidad, sino una esperanza de vida. Cada gota cuenta, y tu ayuda puede marcar la diferencia en su lucha por sobrevivir."</p>
                        <button className="btn btn-secondary" onClick={handleRequestBlood}>Adquirir sangre</button>
                    </div>
                </section>

                {/* Quote */}
                <section className="quote">
                    <p>"Donar sangre no solo salva vidas; es un acto de solidaridad que fortalece nuestra comunidad y ofrece esperanza a quienes más lo necesitan."</p>
                </section>
            </main>

            {/* Footer */}
            <Footer />
        </div>
    )
}

export default HomePage;