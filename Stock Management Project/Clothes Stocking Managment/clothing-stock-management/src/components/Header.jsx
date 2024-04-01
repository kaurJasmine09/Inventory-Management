import { NavLink, useLocation, useNavigate } from "react-router-dom";

function Header({log}) {
    const location = useLocation();
    const navigate = useNavigate();

    const pathMatchRoute = (route) => {
        return route === location.pathname
    }

    const empName = localStorage.getItem('empName');

    const handleLogout = () => {
        localStorage.clear();
        log();
        navigate('/');
    }

    return (
        <>
            <nav className="navbar navbar-dark bg-dark">
                <div className="container">
                    <span className="navbar-brand">CLOTHING STOCK MANAGEMENT</span>
                    <span className="float-right text-light">{empName} {empName &&<span role="button" className="text-success border border-success rounded p-2" onClick={handleLogout}>Log Out</span> }</span>
                </div>
            </nav>
           
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className=" navbar-nav container d-flex justify-content-start">
                    <h6 className="nav-item nav-link p-3"><NavLink to='/stock-records' className={`text-decoration-none ${pathMatchRoute('/stock-records') ? 'text-success' : 'text-dark'}`}>Stock Records</NavLink></h6>
                    <h6 className="nav-item nav-link p-3"><NavLink to='/stock-in' className={`text-decoration-none ${pathMatchRoute('/stock-in') ? 'text-success' : 'text-dark'}`}>Stock In</NavLink></h6>
                    <h6 className="nav-item nav-link p-3"><NavLink to='/stock-out' className={`text-decoration-none ${pathMatchRoute('/stock-out') ? 'text-success' : 'text-dark'}`}>Stock Out</NavLink></h6>
                    <h6 className="nav-item nav-link p-3"><NavLink to='/current-stock' className={`text-decoration-none ${pathMatchRoute('/current-stock') ? 'text-success' : 'text-dark'}`}>Current Stock</NavLink></h6>
                    <h6 className="nav-item nav-link p-3"><NavLink to='/suppliers' className={`text-decoration-none ${pathMatchRoute('/suppliers') ? 'text-success' : 'text-dark'}`}>Suppliers</NavLink></h6>
                    <h6 className="nav-item nav-link p-3"><NavLink to='/customers' className={`text-decoration-none ${pathMatchRoute('/customers') ? 'text-success' : 'text-dark'}`}>Customers</NavLink></h6>
                </div>
            </nav>
        </>
    )
}


export default Header;