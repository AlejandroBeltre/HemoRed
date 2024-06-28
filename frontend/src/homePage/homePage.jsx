import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import '../App.css';
import './homePage.css';
import Dropdown from '../components/dropdownMenu';
import Footer from '../components/footer';
import Headers from '../components/header';
import background from '../assets/images/HomePageBackground.png';

function HomePage() {
    const [isDropdownVisible, setDropdownVisible] = useState(false);
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
                <section className="blood-types">
                    <h2>TIPOS DE SANGRE</h2>
                    <div className="blood-type-grid">
                        {['A', 'B', 'AB', 'O'].map(type => (
                            <div key={type} className="blood-type-icon">
                                <img src={`/path/to/${type.toLowerCase()}-blood-icon.png`} alt={`Blood type ${type}`} />
                            </div>
                        ))}
                    </div>
                </section>

                {/* Donation CTA */}
                <section className="donation-cta">
                    <div className="donation-image">
                        <img src="/path/to/donation-image.jpg" alt="Blood donation" />
                    </div>
                    <div className="donation-text">
                        <p>Con tu donación, puedes transformar el futuro de quienes más lo necesitan. ¡Haz la diferencia hoy!</p>
                        <button className="btn btn-primary">Donar</button>
                    </div>
                </section>

                {/* Testimonials */}
                <section className="testimonials">
                    <h2>TESTIMONIOS</h2>
                    <div className="testimonial-grid">
                        {[1, 2, 3].map(i => (
                            <div key={i} className="testimonial">
                                <img src={`/path/to/testimonial-${i}.jpg`} alt={`Testimonial ${i}`} />
                                <p>Saber que mi pequeña contribución puede salvar vidas me llena de alegría y gratitud.</p>
                            </div>
                        ))}
                    </div>
                </section>

                {/* Need Blood Section */}
                <section className="need-blood">
                    <div className="need-blood-image">
                        <img src="/path/to/need-blood-image.jpg" alt="Need blood" />
                    </div>
                    <div className="need-blood-text">
                        <h2>¿NECESITAS SANGRE?</h2>
                        <p>"Para quienes enfrentan situaciones críticas de salud, comprar sangre no es solo una necesidad, sino una esperanza de vida. Cada gota cuenta, y tu ayuda puede marcar la diferencia en su lucha por sobrevivir."</p>
                        <button className="btn btn-secondary">Adquirir sangre</button>
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