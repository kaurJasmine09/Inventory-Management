import {useEffect, useState } from "react";
import {currentStocks_jssearch} from '../utils/JsSearch';

function CurrentStock() {

    const [currentStocks, setCurrentStocks] = useState([]);
    const [refreshData, setRefreshData] = useState(false);
    const [search, setSearch] = useState("");
    currentStocks_jssearch.addDocuments(currentStocks);

    useEffect(()=>{
        const fetchCurrentStocks = async () => {
            const response = await fetch('https://sockinventory.azurewebsites.net/api/Product');
            const data = await response.json();
            setCurrentStocks(data.data);
        }
        fetchCurrentStocks();
    }, [refreshData])

    // handle search
    const handleSearch = (e) => {
        if(e.target.value === "") {
            setRefreshData(!refreshData);
        }
        setSearch(e.target.value);
        setCurrentStocks(currentStocks_jssearch.search(e.target.value));
    }

    return (
        <div>
            <div className='d-flex justify-content-between'>
            <div className="container d-flex mt-2 mb-2">
            <h4 className="flex-grow-1" >Current Stocks</h4>
                <input type="text" className="form-control w-25" placeholder="Search" value={search} onChange={handleSearch}  />
                </div>
            </div>
            <table className="table table-striped data-table container table-responsive d-flex flex-column mt-3">
                <thead className="data-table">
                    <tr className="row m-0">
                    <th scope='col' className='col-2'>Product ID</th>
                    <th scope='col' className='col-3'>Product Name</th>
                    <th scope='col' className='col-1 px-0'>Category</th>
                    <th scope='col' className='col-1 px-0'>Quantity</th>
                    <th scope='col' className='col-1 px-0'>Size</th>
                    <th scope='col' className='col-4 px-0'>Description</th>
                </tr>
            </thead>
            <tbody className=" scroll w-auto">
                {currentStocks.map(d=>(<tr key={d.productId} className="row m-0">
                    <td className ='col-2'>{d.productId}</td>
                    <td className ='col-3'>{d.productName}</td>
                    <td className ='col-1'>{d.category}</td>
                    <td className ='col-1'>{d.quantity}</td>
                    <td className ='col-1'>{d.size}</td>
                    <td className ='col-4'>{d.description}</td>
                </tr>))}
            </tbody>
        </table>
        </div>
        
    )
}

export default CurrentStock;
