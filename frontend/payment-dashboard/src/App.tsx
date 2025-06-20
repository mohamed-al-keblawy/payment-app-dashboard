import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import PaymentsPage from './pages/PaymentsPage';
import CardsPage from './pages/CardsPage';

function App() {
  return (
    <BrowserRouter>
      <nav style={{ padding: '1rem' }}>
        <Link to="/payments" style={{ marginRight: '1rem' }}>Payments</Link>
        <Link to="/cards">Cards</Link>
      </nav>
      <Routes>
        <Route path="/payments" element={<PaymentsPage />} />
        <Route path="/cards" element={<CardsPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
