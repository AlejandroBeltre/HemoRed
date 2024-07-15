import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Footer from "../components/footer";
import Header from "../components/header";
import "./modifyCampaigns.css";
import { ArrowLeftOutlined, FileImageFilled } from "@ant-design/icons";
import { getCampaignById, updateCampaign } from "../api";

function ModifyCampaigns() {
    const navigate = useNavigate();
    const { campaignId } = useParams();

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
            try {
                const response = await getCampaignById(campaignId);
                console.log('Raw campaign data:', response);
    
                if (!response || typeof response !== 'object') {
                    throw new Error('Invalid response from server');
                }
    
                if (!response.startTimestamp || !response.endTimestamp) {
                    console.error('Missing timestamp data. Available fields:', response);
                    throw new Error('Missing timestamp data');
                }
    
                const startDate = new Date(response.startTimestamp);
                const endDate = new Date(response.endTimestamp);
    
                if (isNaN(startDate) || isNaN(endDate)) {
                    throw new Error('Invalid date format');
                }
    
                setFormData({
                    name: response.campaignName || '',
                    organizer: 'AFPHumano',
                    address: 'Winston churchill, 39',
                    description: response.description || '',
                    startDate: startDate.toISOString().split('T')[0],
                    startTime: startDate.toTimeString().split(' ')[0].slice(0, 5),
                    endDate: endDate.toISOString().split('T')[0],
                    endTime: endDate.toTimeString().split(' ')[0].slice(0, 5),
                    image: response.image || null,
                });
                setIsLoading(false);
            } catch (error) {
                console.error('Failed to fetch campaign data:', error);
                setError('Failed to fetch campaign data: ' + error.message);
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

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const campaignData = new FormData();
            campaignData.append('CampaignID', campaignId);
            campaignData.append('AddressID', 1);
            campaignData.append('OrganizerID', 1);
            campaignData.append('CampaignName', formData.name);
            campaignData.append('Description', formData.description);
            
            const startTimestamp = new Date(`${formData.startDate}T${formData.startTime}`).toISOString();
            const endTimestamp = new Date(`${formData.endDate}T${formData.endTime}`).toISOString();
            
            campaignData.append('StartTimestamp', startTimestamp);
            campaignData.append('EndTimestamp', endTimestamp);
            
            if (formData.image instanceof File) {
                campaignData.append('Image', formData.image);
            }
    
            await updateCampaign(campaignId, campaignData);
            setNotification("¡Campaña actualizada!");
            setTimeout(() => setNotification(""), 2000);
        } catch (error) {
            console.error('Error updating campaign:', error);
            setNotification("Error al actualizar la campaña");
        }
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
                                {formData.image ? formData.image : 'Ningún archivo seleccionado'}
                            </div>
                            <div className="file-input-icon">
                                <FileImageFilled className='file-input-icon' />
                            </div>
                        </div>
                    </div>
                    <div className="button-container">
                        <button type="submit" className="accept-button-blood-inventory">Actualizar</button>
                    </div>
                    {notification    && <div className="notification">{notification}</div>}
                </form>
            </div>
            <Footer />
        </div>
    );
}

export default ModifyCampaigns;