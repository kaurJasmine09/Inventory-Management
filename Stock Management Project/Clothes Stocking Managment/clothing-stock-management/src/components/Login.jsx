import {useState} from 'react';
import {useNavigate} from 'react-router-dom';

import login from '../assets/images/banner.png';

function Login({log}) {
    const [employeeId, setEmployeeId] = useState("");
    const [pin, setPin] = useState("");
    const navigate = useNavigate();

    const handleEmpIdChange = (e) => {
        setEmployeeId(e.target.value);
    }
    
    const handlePinChange = (e) => {
        setPin(e.target.value);
    }

    const handleLogin = async (e) => {
        e.preventDefault();
        const response = await fetch('https://sockinventory.azurewebsites.net/api/Login/Authenticate?employeeId='+employeeId+"&pin="+pin);
        const data = await response.json();

        if(data.success === true) {
            localStorage.clear();
            localStorage.setItem('authenticated', true);
            localStorage.setItem('empName', data.data.firstName+" "+data.data.lastName);
            localStorage.setItem('empId', data.data.employeeId);
            log();
            navigate('/stock-records');
        } else {
            window.alert("Wrong Credentials");
        }
    }
    

    return (
        <div className=' d-flex align-items-top'>
            <div className='col'>
                <img  src={login} alt='login' height='700' />
            </div>
            <form className='col flex-fill m-5 p-5' onSubmit={handleLogin}>
            <h5 className='mb-3 fw-bold'>CLOTHING STOCK MANAGEMENT</h5>
                <h1 className='mb-4'>Login</h1>
                <div className="form-group w-80">
                    <input type="text" className="form-control mb-2 height-50 bg-gray"  placeholder="Employee Id" value={employeeId} onChange={handleEmpIdChange} /><br />
                    <input type="password" className="form-control mb-2 height-50 bg-gray" placeholder="Enter Pin" value={pin} onChange={handlePinChange} /><br />
                </div>
                <button type="submit" className="btn btn-success bg-green w-80 height-50">Login</button>
            </form>
        </div>
    )
}

export default Login;

/*
email: "mprandini0@sohu.com"
employeeId: 100
firstName: "Mair"
lastName: "Prandini"
phone: "1624680669"
pin: 1111
role: "Manager"
*/