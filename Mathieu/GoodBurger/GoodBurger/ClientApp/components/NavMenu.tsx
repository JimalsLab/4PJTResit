import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <nav className="navbar navbar-inverse mynav" id="mynav">
            <div className="container-fluid noborder">
                <div className="navbar-header">
                    <Link className='navbar-brand' to={ '/' }>GoodBurger</Link>
                </div>
                <ul className="nav navbar-nav">
                    <li>
                        <NavLink to={'/'} exact activeClassName='active'>
                            <span className='glyphicon glyphicon-home'></span> Home
                        </NavLink>
                    </li>
                    <li>
                        <NavLink to={'/Cart'} exact activeClassName='active'>
                            <span className='glyphicon glyphicon-shopping-cart'></span> Cart
                        </NavLink>
                    </li>
                    <li>
                        <NavLink to={'/Profile'} exact activeClassName='active'>
                            <span className='glyphicon glyphicon-user'></span> Account
                        </NavLink>
                    </li>
                </ul>
                <form className="navbar-form navbar-left" action="/action_page.php">
                    <div className="input-group">
                        <input type="text" className="form-control" placeholder="Search" name="search"/>
                            <div className="input-group-btn">
                                <button className="btn btn-default" type="submit">
                                    <i className="glyphicon glyphicon-search"></i>
                                </button>
                            </div>
                      </div>
                    </form>
                  </div>
                </nav>;

        //return <div className='main-nav'>
        //        <div className='navbar navbar-inverse'>
        //        <div className='navbar-header'>
        //            <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
        //                <span className='sr-only'>Toggle navigation</span>
        //                <span className='icon-bar'></span>
        //                <span className='icon-bar'></span>
        //                <span className='icon-bar'></span>
        //            </button>
        //            <Link className='navbar-brand' to={ '/' }>GoodBurger</Link>
        //        </div>
        //        <div className='clearfix'></div>
        //        <div className='navbar-collapse collapse'>
        //            <ul className='nav navbar-nav'>
        //                <li>
        //                    <NavLink to={ '/' } exact activeClassName='active'>
        //                        <span className='glyphicon glyphicon-home'></span> Home
        //                    </NavLink>
        //                </li>
        //                <li>
        //                    <NavLink to={ '/counter' } activeClassName='active'>
        //                        <span className='glyphicon glyphicon-education'></span> Counter
        //                    </NavLink>
        //                </li>
        //                <li>
        //                    <NavLink to={ '/fetchdata' } activeClassName='active'>
        //                        <span className='glyphicon glyphicon glyphicon-user'></span> Fetch data
        //                    </NavLink>
        //                </li>
        //            </ul>
        //        </div>
        //    </div>
        //</div>;

    }

}
