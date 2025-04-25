import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import MenuPage from './pages/MenuPage';
import AdminPanel from './pages/AdminPanel';
import AdminLogin from './pages/AdminLogin';
import ProtectedRoute from './components/shared/ProtectedRoute';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>

        <Route path="/" element={<Navigate to="/menu/test-local-id" />} />
        <Route path="/menu/:localId" element={<MenuPage />} />
        
        <Route
          path="/admin/*"
          element={
            <ProtectedRoute>
              <AdminPanel />
            </ProtectedRoute>
          }
        />
        <Route path='/login/' element={<AdminLogin />} />

        {/* TODO: Dodaj rute za admin/superadmin kasnije */}
      </Routes>
    </Router>
  );
};

export default App;
