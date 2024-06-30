import './App.css';
import React, {useState, useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import HomePage from './homePage/homePage';
import RegisterUser from './registerUser/registerUser';
import LoginUser from './logIn/loginUser';
import RecoverPassword from './recoverPassword/recoverPassword';
import ModifyProfile from './modifyProfile/modifyProfile';
import ViewDonationHistory from './viewDonationHistory/viewDonationhistory';
import ViewBanks from './viewBanks/viewBanks';
  
function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<HomePage />}/>
        <Route path="/registerUser" element={<RegisterUser />}/>
        <Route path="/loginUser" element={<LoginUser />}/>  
        <Route path="/loginUser/recoverPassword" element={<RecoverPassword />}/>
        <Route path="/modifyProfile" element={<ModifyProfile />}/>
        <Route path="/viewDonationHistory" element={<ViewDonationHistory />}/>
        <Route path="/viewBanks" element={<ViewBanks />}/>
      </Routes>
    </div>
  );
}

export default App; 
