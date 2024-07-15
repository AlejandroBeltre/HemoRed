import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5178/api',
});

api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// User API calls
export const registerUser = async (userData) => {
    const formData = new FormData();

    for (const key in userData) {
        if (key === 'image') {
            formData.append('image', userData[key]);
        } else {
            formData.append(key, userData[key]);
        }
    }

    try {
        const response = await api.post('/User/Register', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    } catch (error) {
        console.error('Registration error:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const loginUser = async (loginData) => {
    try {
        const response = await api.post('/User/Login', loginData);
        console.log('Login response:', response.data); // Add this line to log the response
        return response.data;
    } catch (error) {
        throw error.response.data;
    }
};
export const logoutUser = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userRole');
};
export const getUsers = () => api.get('/User');
export const getUserById = async (id) => {
    try {
        const response = await api.get(`/User/${id}`);
        return response.data;
    } catch (error) {
        throw error.response.data;
    }
};
export const updateUser = (id, user) => api.put(`/User/${id}`, user);

// Blood Bank API calls
export const getBloodBanks = () => api.get('/BloodBank');
export const createBloodBank = async (bloodBankData) => {
    const formData = new FormData();
    for (const key in bloodBankData) {
        formData.append(key, bloodBankData[key]);
    }
    try {
        const response = await api.post('/BloodBank', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    } catch (error) {
        console.error('Blood bank creation error:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const getBloodBankById = (id) => api.get(`/BloodBank/${id}`);
export const updateBloodBank = (id, data) => {
    return api.put(`/BloodBank/${id}`, data, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};
export const deleteBloodBank = (id) => api.delete(`/BloodBank/${id}`);

// Blood Bag API calls
export const getBloodBags = () => api.get('/BloodBag');
export const createBloodBag = async (bloodBag) => {
    try {
        const response = await api.post('/BloodBag', bloodBag, {
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return response.data;
    } catch (error) {
        console.error('Blood bag creation error:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const getBloodBagById = (id) => api.get(`/BloodBag/${id}`);
export const updateBloodBag = (id, bloodBag) => api.put(`/BloodBag/${id}`, bloodBag);
export const deleteBloodBag = (id) => api.delete(`/BloodBag/${id}`);

// Campaign API calls
export const getCampaigns = async () => {
    try {
        const response = await api.get('/Campaign');
        return response.data.map(campaign => ({
            id: campaign.campaignID,
            addressID: campaign.addressID,
            organizerID: campaign.organizerID,
            name: campaign.campaignName,
            description: campaign.description,
            startTimestamp: campaign.startTimestamp,
            endTimestamp: campaign.endTimestamp,
            image: campaign.image,
            isActive: true // Assuming all campaigns are active by default
        }));
    } catch (error) {
        console.error('Error fetching campaigns:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const createCampaign = (campaign) => api.post('/Campaign', campaign);
export const getCampaignById = async (id) => {
    try {
        const response = await api.get(`/Campaign/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching campaign:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const updateCampaign = (id, campaign) => api.put(`/Campaign/${id}`, campaign);
export const deleteCampaign = (id) => api.delete(`/Campaign/${id}`);

// Request API calls
export const getRequests = () => api.get('/Request');
export const createRequest = async (requestData) => {
    try {
        const response = await api.post('/Request', requestData);
        return response.data;
    } catch (error) {
        console.error('Request creation error:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const getRequestById = (id) => api.get(`/Request/${id}`);
export const updateRequest = (id, request) => api.put(`/Request/${id}`, request);
export const deleteRequest = (id) => api.delete(`/Request/${id}`);

// Address API calls
export const getAddresses = () => api.get('/Address');
export const getAddressById = (id) => api.get(`/Address/${id}`);
export const createAddress = async (addressData) => {
    try {
        const response = await api.post('/Address', addressData, {
            headers: {
                'Content-Type': 'application/json',
            },
        });
        return response.data;
    } catch (error) {
        console.error('Address creation error:', error.response || error);
        throw error.response?.data || error.message;
    }
};
export const updateAddress = (id, address) => api.put(`/Address/${id}`, address);
export const deleteAddress = (id) => api.delete(`/Address/${id}`);

// Blood Type API calls
export const getBloodTypes = () => api.get('/BloodType');
export const getBloodTypeById = (id) => api.get(`/BloodType/${id}`);
export const deleteBloodType = (id) => api.delete(`/BloodType/${id}`);

// Donation API calls
export const getDonations = () => api.get('/Donation');
export const getDonationById = (id) => api.get(`/Donation/${id}`);
export const createDonation = (donation) => api.post('/Donation', donation);
export const updateDonation = (id, donation) => api.put(`/Donation/${id}`, donation);
export const deleteDonation = (id) => api.delete(`/Donation/${id}`);

// Province API calls
export const getProvinces = () => api.get('/Province');
export const getProvinceById = (id) => api.get(`/Province/${id}`);