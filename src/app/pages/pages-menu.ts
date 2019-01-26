import { NbMenuItem } from "@nebular/theme";

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: "Dashboard",
    link: "/pages/dashboard",
    home: true
  },
  {
    title: "Wiadomości",
    link: "/pages/dashboard"
  },
  {
    title: "Ustawienia",
    link: "/pages/orders",
    children: [
      {
        title: "Lista zamówień",
        link: "/pages/orders/list"
      },
      {
        title: "Dodaj nowe zamówienie",
        link: "/pages/orders/new"
      },
      {
        title: "Pozycja X"
      },
      {
        title: "Pozycja Y"
      }
    ]
  }
];
