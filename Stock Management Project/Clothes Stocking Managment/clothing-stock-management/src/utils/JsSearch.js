import * as JsSearch from 'js-search';


// customer search
export const customer_jssearch = new JsSearch.Search('customerId');
customer_jssearch.addIndex('customerId');
customer_jssearch.addIndex('customerName');
customer_jssearch.addIndex('location');
customer_jssearch.addIndex('email');
customer_jssearch.addIndex('phone');

// supplier search
export const supplier_jssearch = new JsSearch.Search('supplierId');
supplier_jssearch.addIndex('supplierId');
supplier_jssearch.addIndex('supplierName');
supplier_jssearch.addIndex('email');
supplier_jssearch.addIndex('phone');

// current stocks search
export const currentStocks_jssearch = new JsSearch.Search('productId');
currentStocks_jssearch.addIndex('productId');
currentStocks_jssearch.addIndex('productName');
currentStocks_jssearch.addIndex('description');
currentStocks_jssearch.addIndex('size');
currentStocks_jssearch.addIndex('category');
currentStocks_jssearch.addIndex('quantity');

// transactions search
export const transactions_jssearch = new JsSearch.Search('transactionNumber');
transactions_jssearch.addIndex('productName');
transactions_jssearch.addIndex('quantity');
transactions_jssearch.addIndex('date');
transactions_jssearch.addIndex('transactionNumber');
transactions_jssearch.addIndex('transactionId');
transactions_jssearch.addIndex('comments');
transactions_jssearch.addIndex('type');
transactions_jssearch.addIndex('supplierName');
transactions_jssearch.addIndex('customerName');
transactions_jssearch.addIndex('employeeName');





