import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useEffect, useState } from "react";

import {InventoryProvider} from "./Context/InventoryContext";
import Header from "./components/Header";
import Footer from "./components/Footer";
import StockRecords from "./components/StockRecords";
import StockIn from "./components/StockIn";
import StockOut from "./components/StockOut";
import CurrentStock from "./components/CurrentStock";
import Login from "./components/Login";
import Customers from "./components/Customers";
import Suppliers from "./components/Suppliers";
import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  const [authenticated, setAuthenticated] = useState(false);

  useEffect(()=>{
    if(localStorage.getItem('authenticated')) {
      setAuthenticated(true);
    }
  }, []);

  const log = () => {
    localStorage.getItem('authenticated') ? 
      setAuthenticated(true) : setAuthenticated(false);
  }
  
  console.log(authenticated);

  return (
    <InventoryProvider>
      <Router>
        {authenticated ? <Header log={log} /> : <></>}
          <div className=''>
            <Routes>
              <Route exact path="/" element={<Login log={log} />} />
              <Route element={<ProtectedRoute authenticated={authenticated} />}>
                <Route exact path="/stock-records" element={<StockRecords />} />
                <Route exact path="/stock-in" element={<StockIn />} />
                <Route exact path="/stock-out" element={<StockOut />} />
                <Route exact path="/suppliers" element={<Suppliers />} />
                <Route exact path="/customers" element={<Customers />} />
                <Route exact path="/current-stock" element={<CurrentStock />} />
              </Route>
            </Routes>
          </div>
        <Footer />
      </Router>
    </InventoryProvider>
  );
}

export default App;
