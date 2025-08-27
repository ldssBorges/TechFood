import { ReactNode } from "react";
import { Navigate, useLocation, useMatches } from "react-router";
import { User } from "../../models";

export const SecureRoute = ({ children }: { children: ReactNode }) => {
  const token = localStorage.getItem("token");
  const user = JSON.parse(localStorage.getItem("user") || "{}") as User;

  const matches = useMatches();
  const { roles } = (matches[matches.length - 1]?.handle as any) || {};

  const location = useLocation();

  if (!token) {
    return <Navigate to="/signin" state={{ from: location }} replace />;
  }

  if (roles && !roles.includes(user.role)) {
    return <Navigate to="/forbidden" replace />;
  }

  return children;
};
