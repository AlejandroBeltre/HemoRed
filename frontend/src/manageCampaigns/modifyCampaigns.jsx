import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Footer from "../components/footer";
import Header from "../components/header";
import "./modifyCampaigns.css";
import { ArrowLeftOutlined, FileImageFilled } from "@ant-design/icons";
import diaDelDonador from '../assets/images/diaDelDonador.png';
import regalaVida from '../assets/images/regalaVida.png';
import sangreParaTodos from '../assets/images/sangreParaTodos.png';

function ModifyCampaigns() {
    const navigate = useNavigate();
    const { campaignId } = useParams();

    const [campaign, setCampaign] = useState(null);
    const [formData, setFormData] = useState({
        name: '',
        organizer: '',
        address: '',
        description: '',
        startDate: '',
        startTime: '',
        endDate: '',
        endTime: '',
        image: null,
    });
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [notification, setNotification] = useState("");

    useEffect(() => {
        const fetchCampaign = async () => {
            setIsLoading(true);
            try {
                // Simulate fetching data from an API
                await new Promise(resolve => setTimeout(resolve, 1000));
                const campaigns = [
                    {
                        id: '1',
                        name: "¡Sangre para todos!",
                        organizer: "Cruz Roja Americana",
                        address: "123 Elm Street, Springfield, IL 62704, USA",
                        description: "Campaña para donar sangre",
                        startDate: "2023-06-13",
                        startTime: "07:00",
                        endDate: "2023-06-15",
                        endTime: "15:00",
                        image: sangreParaTodos,
                    },
                    {
                        id: '2',
                        name: "Regala vida",
                        organizer: "Servicios de Sangre de Canadá",
                        address: "456 Maple Avenue, Toronto, ON M4C 1B5, Canada",
                        description: "Campaña para donar sangre",
                        startDate: "2024-06-14",
                        startTime: "09:00",
                        endDate: "2024-06-15",
                        endTime: "17:00",
                        image: regalaVida,
                    },
                    {
                        id: '3',
                        name: "Dia del donador",
                        organizer: "Servicios Nacional de Salud de la Sangre y Transplantes",
                        address: "789 Pine Road, London, SW1A 1AA, United Kingdom",
                        description: "Campaña para donar sangre",
                        startDate: "2024-01-12",
                        startTime: "09:00",
                        endDate: "2024-01-12",
                        endTime: "14:00",
                        image: diaDelDonador,
                    }
                ];
                const mockCampaign = campaigns.find(campaign => campaign.id === campaignId);
                setCampaign(mockCampaign);
                setFormData(mockCampaign);
            } catch (error) {
                setError('Failed to fetch campaign data');
            } finally {
                setIsLoading(false);
            }
        };

        fetchCampaign();
    }, [campaignId]);

    const handleBack = () => {
        navigate(-1);
    };

    const handleChange = (e) => {
        const { name, value, type, files } = e.target;
        if (type === "file") {
            setFormData({
                ...formData,
                [name]: files[0]
            });
        } else {
            setFormData({
                ...formData,
                [name]: value
            });
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Form submitted:', formData);
        setNotification("¡Campaña actualizada!");
        setTimeout(() => setNotification(""), 2000);
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <Header />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className="modify-blood-bank-container">
                <h1>Editar campaña</h1>
                <form className="add-blood-to-bank-inventory-form" onSubmit={handleSubmit}>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="name">Nombre</label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            className="form-control"
                            value={formData.name}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="organizer">Organizador</label>
                        <input
                            type="text"
                            id="organizer"
                            name="organizer"
                            className="form-control"
                            value={formData.organizer}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="address">Dirección</label>
                        <input
                            type="text"
                            id="address"
                            name="address"
                            className="form-control"
                            value={formData.address}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="description">Descripción</label>
                        <textarea
                            id="description"
                            name="description"
                            className="form-control"
                            value={formData.description}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="startDate">Fecha/Hora de inicio</label>
                        <div className="date-time-inputs">
                            <input
                                type="date"
                                id="startDate"
                                name="startDate"
                                className="form-control"
                                value={formData.startDate}
                                onChange={handleChange}
                                required
                            />
                            <input
                                type="time"
                                id="startTime"
                                name="startTime"
                                className="form-control"
                                value={formData.startTime}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="endDate">Fecha/Hora de finalización</label>
                        <div className="date-time-inputs">
                            <input
                                type="date"
                                id="endDate"
                                name="endDate"
                                className="form-control"
                                value={formData.endDate}
                                onChange={handleChange}
                                required
                            />
                            <input
                                type="time"
                                id="endTime"
                                name="endTime"
                                className="form-control"
                                value={formData.endTime}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="image">Foto</label>
                        <div className="file-input">
                            <input
                                type="file"
                                id="image"
                                name="image"
                                onChange={handleChange}
                                accept="image/*"
                            />
                            <div className="file-input-text">
                                {formData.image ? formData.image : 'Ningn archivo seleccionado'}
                            </div>
                            <div className="file-input-icon">
                                <FileImageFilled className='file-input-icon' />
                            </div>
                        </div>
                    </div>
                    <div className="button-container">
                        <button type="submit" className="accept-button-blood-inventory">Actualizar</button>
                    </div>
                </form>
                {notification && <div className="notification">{notification}</div>}
            </div>
            <Footer />
        </div>
    );
}

export default ModifyCampaigns;