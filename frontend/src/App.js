import './App.css';
import React, {useState, useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import HomePage from './homePage/homePage';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<HomePage />}/>
      </Routes>
    </div>
  );
}

export default App; 
