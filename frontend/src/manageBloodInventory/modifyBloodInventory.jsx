import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import './modifyBloodInventory.css';
import Headers from "../components/header";
import Footer from "../components/footer";
import { ArrowLeftOutlined } from "@ant-design/icons";
import Select from 'react-select';

function ModifyBloodInventory() {
    const navigate = useNavigate();
    const { bankId } = useParams();

    const [bloodBank, setBloodBank] = useState(null);
    const [selectedBloodType, setSelectedBloodType] = useState(null);
    const [quantity, setQuantity] = useState('');
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedBloodBankName, setSelectedBloodBankName] = useState("");

    useEffect(() => {
        const fetchBloodBank = async () => {
            setIsLoading(true);
            try {
                // Simulate fetching data from an API
                await new Promise(resolve => setTimeout(resolve, 1000));
                const mockBloodBanks = [
                    {
                        id: 1,
                        name: "Blood Bank of Alaska",
                        bloodTypes: [
                            { type: "A+", bags: 23 },
                            { type: "A-", bags: 100 },
                            { type: "B+", bags: 23 },
                            { type: "B-", bags: 100 },
                            { type: "AB+", bags: 23 },
                            { type: "AB-", bags: 100 },
                            { type: "O+", bags: 23 },
                            { type: "O-", bags: 0 },
                        ]
                    },
                    {
                        id: 2,
                        name: "Centro de la sangre y especialidades",
                        bloodTypes: [
                            { type: "A+", bags: 120 },
                            { type: "A-", bags: 0 },
                            { type: "B+", bags: 20 },
                            { type: "B-", bags: 55 },
                            { type: "AB+", bags: 10 },
                            { type: "AB-", bags: 66 },
                            { type: "O+", bags: 35 },
                            { type: "O-", bags: 21 },
                        ]
                    },
                ];
                const fetchedBloodBank = mockBloodBanks.find(bank => bank.id === parseInt(bankId));
                if (fetchedBloodBank) {
                    setBloodBank(fetchedBloodBank);
                    setSelectedBloodBankName(fetchedBloodBank.name);
                } else {
                    setError("Blood bank not found");
                }
            } catch (err) {
                console.error("Error fetching blood bank data:", err);
                setError("Failed to fetch blood bank data");
            } finally {
                setIsLoading(false);
            }
        };

        fetchBloodBank();
    }, [bankId]);

    const handleBack = () => {
        navigate(-1);
    };

    const handleBloodTypeChange = (selectedOption) => {
        setSelectedBloodType(selectedOption);
        const bloodType = bloodBank.bloodTypes.find(type => type.type === selectedOption.value);
        if (bloodType) {
            setQuantity(bloodType.bags);
        } else {
            setQuantity('');
        }
    };

    const handleQuantityChange = (e) => {
        setQuantity(e.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (selectedBloodType && quantity) {
            const updatedBloodBank = { ...bloodBank };
            const bloodTypeIndex = updatedBloodBank.bloodTypes.findIndex(type => type.type === selectedBloodType.value);
            if (bloodTypeIndex !== -1) {
                updatedBloodBank.bloodTypes[bloodTypeIndex].bags = parseInt(quantity);
                setBloodBank(updatedBloodBank);
                // Here you can also send the updated blood bank data to the server if required
            }
        }
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    const bloodTypeOptions = bloodBank.bloodTypes.map((bloodType) => ({
        value: bloodType.type,
        label: bloodType.type,
    }));

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className="add-blood-to-inventory-container">
                <h1>Modificar Inventario de Sangre</h1>
                <form className="add-blood-to-bank-inventory-form" onSubmit={handleSubmit}>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="bankName">Banco de sangre:</label>
                        <input type="text" value={selectedBloodBankName} readOnly />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="bloodType">Tipo de Sangre:</label>
                        <Select
                            id="bloodType"
                            value={selectedBloodType}
                            onChange={handleBloodTypeChange}
                            options={bloodTypeOptions}
                            placeholder="Seleccione un tipo de sangre"
                            className="form-control"
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="quantity">Cantidad de Bolsas:</label>
                        <input
                            type="number"
                            id="quantity"
                            onChange={handleQuantityChange}
                            value={quantity}
                            className="quantity-input"
                        />
                    </div>
                    <button type="submit" className="accept-button-blood-inventory">Actualizar</button>
                </form>
            </div>
            <Footer />
        </div>
    );
}

export default ModifyBloodInventory;
