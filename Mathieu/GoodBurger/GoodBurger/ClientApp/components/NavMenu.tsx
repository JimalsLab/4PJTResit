import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { NavInfo } from './NavInfo';

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
                <div className="navbar-right">
                    <NavInfo></NavInfo>
                </div>
                  </div>
                </nav>;

    }

}
