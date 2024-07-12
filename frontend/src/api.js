import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5178/api',
});

// User API calls
export const registerUser = (user) => api.post('/User/Register', user);
export const loginUser = (user) => api.post('/User/Login', user);
export const getUsers = () => api.get('/User');
export const getUserById = (id) => api.get(`/User/${id}`);
export const updateUser = (id, user) => api.put(`/User/${id}`, user);

// Blood Bank API calls
export const getBloodBanks = () => api.get('/BloodBank');
export const createBloodBank = (bloodBank) => api.post('/BloodBank', bloodBank);
export const getBloodBankById = (id) => api.get(`/BloodBank/${id}`);
export const updateBloodBank = (id, bloodBank) => api.put(`/BloodBank/${id}`, bloodBank);
export const deleteBloodBank = (id) => api.delete(`/BloodBank/${id}`);

// Blood Inventory API calls
export const getBloodInventory = () => api.get('/BloodInventory');
export const addBloodToInventory = (blood) => api.post('/BloodInventory', blood);
export const getBloodInventoryById = (id) => api.get(`/BloodInventory/${id}`);
export const updateBloodInventory = (id, blood) => api.put(`/BloodInventory/${id}`, blood);
export const deleteBloodInventory = (id) => api.delete(`/BloodInventory/${id}`);

// Campaign API calls
export const getCampaigns = () => api.get('/Campaign');
export const createCampaign = (campaign) => api.post('/Campaign', campaign);
export const getCampaignById = (id) => api.get(`/Campaign/${id}`);
export const updateCampaign = (id, campaign) => api.put(`/Campaign/${id}`, campaign);
export const deleteCampaign = (id) => api.delete(`/Campaign/${id}`);

// Request API calls
export const getRequests = () => api.get('/Request');
export const createRequest = (request) => api.post('/Request', request);
export const getRequestById = (id) => api.get(`/Request/${id}`);
export const updateRequest = (id, request) => api.put(`/Request/${id}`, request);
export const deleteRequest = (id) => api.delete(`/Request/${id}`);

// Address API calls
export const getAddresses = () => api.get('/Address');
export const getAddressById = (id) => api.get(`/Address/${id}`);
export const createAddress = (address) => api.post('/Address', address);
export const updateAddress = (id, address) => api.put(`/Address/${id}`, address);
export const deleteAddress = (id) => api.delete(`/Address/${id}`);

// Blood Bag API calls
export const getBloodBags = () => api.get('/BloodBag');
export const getBloodBagById = (id) => api.get(`/BloodBag/${id}`);
export const createBloodBag = (bloodBag) => api.post('/BloodBag', bloodBag);
export const updateBloodBag = (id, bloodBag) => api.put(`/BloodBag/${id}`, bloodBag);
export const deleteBloodBag = (id) => api.delete(`/BloodBag/${id}`);

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