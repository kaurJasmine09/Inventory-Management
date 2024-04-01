import {useEffect, useState} from 'react';
import {customer_jssearch} from '../utils/JsSearch';

function Customers() {
    const [data, setData] = useState([]);
    const [refreshData, setRefreshData] = useState(false);
    const [search, setSearch] = useState("");
    customer_jssearch.addDocuments(data);

    useEffect(()=>{
        const fetchEntityData = async () => {
            const response = await fetch('https://sockinventory.azurewebsites.net/api/Customer');
            const data = await response.json();
            setData(data.data);
        };
        fetchEntityData();
    }, [refreshData]);

    const handleSearch = (e) => {
        if(e.target.value === "") {
            setRefreshData(!refreshData);
        }
        setSearch(e.target.value);
        setData(customer_jssearch.search(e.target.value));
    }
    
    return (
        <>
            <div>
            <div className='d-flex justify-content-between'>
            <div className="container d-flex mt-2 mb-2">
            <h4 className="flex-grow-1" >Customers</h4>
                    <input type="text" className="form-control w-25" placeholder="Search" value={search} onChange={handleSearch}  />
                    </div>
                </div>
                <table className="table table-striped data-table container table-responsive d-flex flex-column mt-3">
                <thead className="data-table">
                    <tr className="row m-0">
                            <th scope='col' className='col-2'>Customer ID</th>
                            <th scope='col' className='col-3'>Customer Name</th>
                            <th scope='col' className='col-2 px-0'>Location</th>
                            <th scope='col' className='col-3 px-0'>Email</th>
                            <th scope='col' className='col-2 px-0'>Phone</th>
                        </tr>
                    </thead>
                    <tbody className=" scroll w-auto">
                        {data.map(d=>(<tr key={d.customerId} className="row m-0">
                                <td className='col-2'>{d.customerId}</td>
                                <td className='col-3'>{d.customerName}</td>
                                <td className='col-2'>{d.location}</td>
                                <td className='col-3'>{d.email}</td>
                                <td className='col-2'>{d.phone}</td>
                       </tr>))}
                    </tbody>
                </table>
            </div>
        </>
    )
}

export default Customers;