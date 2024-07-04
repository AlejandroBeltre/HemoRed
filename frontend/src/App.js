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
import ViewBankDetails from './viewBanks/viewBankDetails';
import ViewBloodRequestStatus from './viewBloodRequestStatus/viewBloodRequestStatus';
import ManageBloodInventory from './manageBloodInventory/manageBloodInvetory';
import AddBloodToInventory from './manageBloodInventory/addBloodToInventory';
import ModifyBloodInventory from './manageBloodInventory/modifyBloodInventory';
  
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
        <Route path="/viewBanks/viewBankDetails/:id" element={<ViewBankDetails />}/>
        <Route path="/viewBloodRequestStatus" element={<ViewBloodRequestStatus />}/>
        <Route path="/manageBloodInventory" element={<ManageBloodInventory />}/>
        <Route path="/manageBloodInventory/addBloodToInventory" element={<AddBloodToInventory/>}/>
        <Route path="/manageBloodInventory/modifyBloodInventory/:bankId" element={<ModifyBloodInventory/>}/>
      </Routes>
    </div>
  );
}

export default App; 
