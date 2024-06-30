import './App.css';
import React, {useState, useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import HomePage from './homePage/homePage';
import RegisterUser from './registerUser/registerUser';
import LoginUser from './logIn/loginUser';
import RecoverPassword from './recoverPassword/recoverPassword';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<HomePage />}/>
        <Route path="/registerUser" element={<RegisterUser />}/>
        <Route path="/loginUser" element={<LoginUser />}/>  
        <Route path="/recoverPassword" element={<RecoverPassword />}/>
      </Routes>
    </div>
  );
}

export default App; 
