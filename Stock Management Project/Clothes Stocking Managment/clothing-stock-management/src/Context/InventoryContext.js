import { createContext, useEffect, useState } from "react";

const InventoryContext = createContext();

export const InventoryProvider = ({children}) => {
    const [products, setProducts] = useState([]);
    const [customers, setCustomers] = useState([]);
    const [suppliers, setSuppliers] = useState([]);
    const [transactions, setTransactions] = useState([]);

    useEffect(()=>{
        fetchProducts();
        fetchCustomers();
        fetchSuppliers();
        fetchTransactions();
    }, []);

    // Get current products
    const fetchProducts = async () => {
        const response = await fetch('https://sockinventory.azurewebsites.net/api/Product');
        const data = await response.json();
        setProducts(data);
    }

    // get customers
    const fetchCustomers = async () => {
        const response = await fetch('https://sockinventory.azurewebsites.net/api/Customer');
        const data = await response.json();
        setCustomers(data.data);
    };

    const fetchSuppliers = async () => {
        const response = await fetch('https://sockinventory.azurewebsites.net/api/Supplier');
        const data = await response.json();
        setSuppliers(data.data);
    };

    const fetchTransactions = async () => {
        const response = await fetch('https://sockinventory.azurewebsites.net/api/Stock');
        const data = await response.json();
        if(data.data) {
            setTransactions(data.data);
        }
        
    }

    const addStockRecord = async (newRecord) => {
        console.log(newRecord);
        const response = await fetch('https://sockinventory.azurewebsites.net/api/Stock/StockDetail', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newRecord)
        })

        const data = await response.json();
        return data;
    }

    const stockOutProduct = async (updatedRecord) => {
        const response = await fetch('/stocks?productId='+updatedRecord.productId, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedRecord)
        });

        const data = await response.json();
        return data;
    }

    const fetchProductDetails =  async (productId) => {
        const records = await fetch('https://sockinventory.azurewebsites.net/api/Product/'+ productId);
        const data = await records.json();
        return data;
    }

    return (
        <InventoryContext.Provider value={{transactions, suppliers, customers, products, stockOutProduct, addStockRecord, fetchProductDetails}}>{children}</InventoryContext.Provider>
    )
}

export default InventoryContext;

