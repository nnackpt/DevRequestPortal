"use client"

import PersonOutlined from "@mui/icons-material/PersonOutlined";
import LockOutlined from "@mui/icons-material/LockOutlined";
import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import WindowOutlined from "@mui/icons-material/WindowOutlined";
import LoginOutlined from "@mui/icons-material/LoginOutlined";
import BadgeOutlined from "@mui/icons-material/BadgeOutlined";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { Box } from "@mui/material";
import { getConfig } from "@/lib/config";

export default function Home() {
    const router = useRouter()
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [showPassword, setShowPassword] = useState(false)
    const [error, setError] = useState<string | null>(null)
    const [loadingManual, setLoadingManual] = useState(false)
    const [loadingSSO, setLoadingSSO] = useState(false)

    // Manual login
    const handleLogin = async () => {
        if (!username || !password) {
            setError("Please enter your Windows username and password.")
            return
        }
        setError(null)
        setLoadingManual(true)
        try {
            const { API_BASE_URL } = await getConfig()
            const res = await fetch(`${API_BASE_URL}/api/authentication/login`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username, password }),
            });
            if (!res.ok) {
                const data = await res.json()
                setError(data?.error ?? "Invalid username or password.")
                return
            }
            const { token } = await res.json()
            localStorage.setItem("token", token)
            router.push("/proposals")
        } catch {
            setError("Cannot connect to server. Please try again.")
        } finally {
            setLoadingManual(false)
        }
    }

    // SSO
    const handleSSO = async () => {
        setError(null)
        setLoadingSSO(true)
        try {
            const { API_BASE_URL } = await getConfig()
            const res = await fetch(`${API_BASE_URL}/api/authentication/sso`, {
                credentials: "include",
            });
            if (!res.ok) {
                setError("SSO sign-in failed. Please try manual login.")
                return
            }
            const { token } = await res.json()
            localStorage.setItem("token", token)
            router.push("/proposals")
        } catch {
            setError("SSO is unavailable. Please use manual login.")
        } finally {
            setLoadingSSO(false)
        }
    }

    return (
        <Box></Box>
    )
}