import {MenuHeader} from './models/menuHeader';
import {RoleMenuElement} from './models/roleMenuElement';

export const menu: MenuHeader[] = [
  {
    name: 'Examples',
    elements: [
      {
        path: '/products',
        name: 'Grid Example',
        roles: []
      },
      {
        path: '/products/new',
        name: 'Form Example',
        roles: ['Admin']
      }
    ] as RoleMenuElement[]
  }
];
