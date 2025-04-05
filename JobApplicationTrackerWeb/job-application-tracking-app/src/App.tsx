import './css/app.css';
import { Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import AddApplication from './pages/AddApplication';
import Application from './pages/Application';

function App() {
   return (
      <>
         <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/add" element={<AddApplication />} />
            <Route path="/application/:applicationId" element={<Application />} />
         </Routes>
      </>
   );
}

export default App;
