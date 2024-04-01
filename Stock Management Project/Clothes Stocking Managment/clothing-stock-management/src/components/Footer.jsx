function Footer() {

    const date = new Date();

    return (
        <footer className=" bg-footer fixed-bottom" >
            <div className="container p-2 d-flex justify-content-between">
                <span>Copyright {date.getFullYear()}</span>
                <span><b>Developed by</b> Jasmine, Pamela, Jaspal, Minh, Karamjeet</span>
            </div>    
        </footer>
    )
}

export default Footer;