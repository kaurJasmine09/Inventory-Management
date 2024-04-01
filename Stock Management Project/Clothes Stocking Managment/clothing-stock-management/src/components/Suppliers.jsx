import {useEffect, useState} from 'react';
import {supplier_jssearch} from '../utils/JsSearch';

function Suppliers() {
    const [data, setData] = useState([]);
    const [refreshData, setRefreshData] = useState(false);
    const [search, setSearch] = useState("");
    supplier_jssearch.addDocuments(data);

    useEffect(()=>{
        const fetchEntityData = async () => {
            const response = await fetch('https://sockinventory.azurewebsites.net/api/Supplier');
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
        setData(supplier_jssearch.search(e.target.value));
    }
    
    return (
        <>
            <div>
            <div className='d-flex justify-content-between'>
            <div className="container d-flex mt-2 mb-2">
            <h4 className="flex-grow-1" >Suppliers</h4>
                    <input type="text" className="form-control w-25" placeholder="Search" value={search} onChange={handleSearch}  />
                </div>
                </div>
                <table className="table table-striped data-table container table-responsive d-flex flex-column mt-3">
                <thead className="data-table">
                    <tr className="row m-0">
                            <th scope='col' className='col-2'>Supplier ID</th>
                            <th scope='col' className='col-4'>Supplier Name</th>
                            <th scope='col' className='col-4 px-0'>Email</th>
                            <th scope='col' className='col-2 px-0'>Phone</th>
                        </tr>
                    </thead>
                    <tbody className="scroll w-auto">
                        {data.map(d=>(<tr key={d.supplierId} className="row m-0">
                                <td className='col-2'>{d.supplierId}</td>
                                <td className='col-4'>{d.supplierName}</td>
                                <td className='col-4'>{d.email}</td>
                                <td className='col-2'>{d.phone}</td>
                        </tr>))}
                    </tbody>
                </table>
            </div>
        </>
    )
}

export default Suppliers;