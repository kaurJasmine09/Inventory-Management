import {useContext, useState, useEffect} from 'react';
import InventoryContext from '../Context/InventoryContext';
import check from '../assets/images/check.png';

function StockOut() {

    const {addStockRecord, fetchProductDetails} = useContext(InventoryContext);

    const [productId, setProductId] = useState("");
    const [productName, setProductName] = useState("");
    const [category, setCategory] = useState([]);
    const [customer, setCustomer] = useState([]);
    const [selectedCustomer, setSelectedCustomer] = useState("");
    const [quantity, setQuantity] = useState(0);
    const [description, setDescription] = useState("");
    const [comments, setComments] = useState("");
    const [currentQuantity, setCurrentQuantity] = useState("");
    const [showTransElement, setshowTransElement] = useState(false);
    const [transactionId, setTransactionId] = useState("");

    const clearFields = () => {
        
        // clear the fields
        setProductId("");
        setProductName("");
        setCategory("");
        setQuantity("");
        setDescription("");
        setSelectedCustomer("");
        setComments("");
        setCurrentQuantity("");
        setshowTransElement(false);
    }

    useEffect(()=>{
        const fetchCustomers = async () => {
            clearFields();
            const response = await fetch('https://sockinventory.azurewebsites.net/api/Customer');
            const data = await response.json();
            setCustomer(data.data);
        };
        fetchCustomers();
    }, [])

    const handleProductIdChange = async (e) => {
        setProductId(e.target.value);
        if(e.target.value.length<1) {
            clearFields();
            return;
        }
        const response = await fetchProductDetails(e.target.value);
        if(response.success) {
            const productDetails = response.data;
            setProductName(productDetails.productName);
            setCategory(productDetails.category);
            setQuantity(0);
            setCurrentQuantity(productDetails.quantity);
            setDescription(productDetails.description);
        }
        else {
            alert('No product with id ' + e.target.value + ' was found!');
            clearFields();
        }
        setshowTransElement(false);
    } 

    const handleCustomerChange = (e) => {
        setSelectedCustomer(e.target.value);
        setshowTransElement(false);
    }

    const handleQuantityChange = (e) => {
        setQuantity(e.target.value);
        setshowTransElement(false);
    }
    
    const handleCommentsChange = (e) => {
        setComments(e.target.value);
        setshowTransElement(false);
    }

    const handleStockOut = async (e) => {
        e.preventDefault();

        // validations
        if(productId.trim()==="" || quantity<=0 || isNaN(quantity)) {
            window.alert("Please give all necessary fields");
            return;
        }
        const employeeId = localStorage.getItem('empId');
        const newStock = {
            "employeeId": parseInt(employeeId),
            "productId": parseInt(productId),
            "quantity": parseInt(quantity) * -1,
            "comments": comments,
            "type": selectedCustomer ? "OUT" : "ADJUST",
            "date": new Date(),
            "customerId": selectedCustomer == null ? null : parseInt(selectedCustomer),
        }
        const response = await addStockRecord(newStock);
        if(response.success == true){            
            const id = productId;
            clearFields();
            setProductId(id);
            setTransactionId(response.data.transctionId);
            setshowTransElement(true);            
            const productResponse = await fetchProductDetails(id);
            if(productResponse.success) {
                const productDetails = productResponse.data;
                setProductName(productDetails.productName);
                setCategory(productDetails.category);
                setQuantity(0);
                setCurrentQuantity(productDetails.quantity);
                setDescription(productDetails.description);
            }
        }

    }

    return (
        <div className="container d-flex">
            <form className="col-3 m-3" onSubmit={handleStockOut}>
                <input value={productId} onChange={handleProductIdChange} type="text" className="form-control form-control-lg mb-3" placeholder="Product ID"/>
                <input value={productName} type="text" readOnly="readonly" className="form-control form-control-lg mb-3" placeholder="Product Name"/>
                <input value={category} type="text" readOnly="readonly" className="form-control form-control-lg mb-3" placeholder="Category Name"/>
                <select value={selectedCustomer} className="form-control form-control-lg custom-select custom-select-lg mb-3" onChange={handleCustomerChange}>
                    <option value="">Select Customer</option>
                    {customer.map(i=>(<option key={i.customerId} value={i.customerId}>{i.customerName}</option>))}
                </select>
                <input value={quantity} onChange={handleQuantityChange} type="text" className="form-control form-control-lg mb-3" placeholder="Quantity"/>
                <input maxLength={50} className={" form-control border-gray form-control-lg mb-3"+(comments?"text-dark":"text-muted")} value={comments} onChange={handleCommentsChange} placeholder="Comment..." />
                <input type="submit" className="btn btn-success bg-green w-100 mb-3 mt-3" value="Stock Out" />
            </form> 
            <div className="col-7 m-3">
                <p className={(showTransElement === true?"eleshow":"elehide")}> <img  src={check} alt='login' height='20px' className="mr-1" /> Record updated transaction id <span className="fw-bold">{transactionId}</span></p> 
                <p className={(category?"spanshow":"spanhide")} type="text"><span className="text-dark pb-3 fw-bold fs-5 d-block">{productName} 
                </span><span>Category:  <span className="badge bg-dark rounded-pill"> {category} </span> 
                 </span>  Quantity: <span className="badge bg-dark rounded-pill"> {currentQuantity} </span></p>
                <p className={"w-100 h-75 description-box "+(description?"text-dark":"text-muted")} readOnly="readonly" ><span  className={(description?"spanhide":"spanshow")} >Add Product ID to view product details</span>{description}  </p>
            </div>
        </div>
    ) 
}

export default StockOut;