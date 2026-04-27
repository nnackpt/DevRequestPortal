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
import { Box, Button, Card, CardContent, Chip, CircularProgress, Divider, Typography } from "@mui/material";
import { getConfig } from "@/lib/config";
import Image from "next/image";

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
        <Box
            sx={{
                minHeight: "100vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                background: "linear-gradient(135deg, #f5f7fa 0%, #e8eef9 100%)",
                px: 2
            }}
        >
            <Card
                elevation={0}
                sx={{
                    width: "100%",
                    maxWidth: 420,
                    borderRadius: 4,
                    border: "1px solid",
                    borderColor: "grey.200",
                    boxShadow: "0 8px 40px rgba(22,71,153,0.10)"
                }}
            >
                <CardContent sx={{ p: 4 }}>
                    {/* Logo/Title */}
                    <Box sx={{ textAlign: "center", mb: 3.5 }}>
                        <Box
                            sx={{
                                width: 64,
                                height: 64,
                                borderRadius: 3,
                                bgcolor: "primary.main",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "center",
                                mx: "auto",
                                mb: 2,
                                boxShadow: "0 4px 16px rgba(22,71,153,0.25)"
                            }}
                        >
                            <Image 
                                src="/autoliv_logo.png"
                                alt="Autoliv"
                                width={40}
                                height={40}
                            />
                        </Box>
                        <Typography variant="h6" sx={{ fontWeight: 700, color: "primary.main" }}>
                            Dev Request Portal
                        </Typography>
                        <Typography variant="body2" sx={{ color: "text.secondary", mt: 0.5 }}>
                            Sign in to continue
                        </Typography>
                    </Box>

                    {/* SSO Button */}
                    <Button
                        fullWidth
                        variant="contained"
                        size="large"
                        startIcon={loadingSSO ? <CircularProgress size={18} color="inherit" /> : <WindowOutlined />}
                        onClick={handleSSO}
                        disabled={loadingSSO || loadingManual}
                        sx={{
                            mb: 2.5,
                            py: 1.4,
                            bgcolor: "primary.main",
                            "&:hover": { bgcolor: "#0f3470" },
                            boxShadow: "0 4px 14px rgba(22,71,153,0.30)"
                        }}
                    >
                        {loadingSSO ? "Signing in..." : "Sign in with Windows SSO"}
                    </Button>

                    {/* Divider */}
                    <Divider sx={{ mb: 2.5 }}>
                        <Chip 
                            label="or sign in manually"
                            size="small"
                            sx={{
                                fontSize: 11,
                                color: "text.secondary",
                                bgcolor: "grey.300"
                            }}
                            variant="outlined"
                        />
                    </Divider>

                    {/* User info chip */}
                    <Box sx={{ display: "flex", alignItems: "center", gap: 1, mb: 2 }}>
                        <BadgeOutlined sx={{ fontSize: 16, color: "primary.main"}} />
                        <Typography variant="caption" color="text.secondary">
                            Sign in as Windows User
                        </Typography>
                    </Box>

                    {/* Error */}
                </CardContent>
            </Card>
        </Box>
    )
}