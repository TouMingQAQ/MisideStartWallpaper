package com.misideStart.wallpaper;

import android.service.wallpaper.WallpaperService;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceHolder;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerForActivityOrService;

public class UnityWallpaperService extends WallpaperService {
    private static final String TAG = "UnityWallpaperService";
    private UnityPlayerForActivityOrService service;
    private UnityWallpaperEngine engine;
    @Override
    public Engine onCreateEngine() {
        engine =  new UnityWallpaperEngine();
        service = new UnityPlayerForActivityOrService(getApplicationContext());
        engine.service = service;
        return engine;
    }

    private class UnityWallpaperEngine extends Engine {
        public UnityPlayerForActivityOrService service;
        private boolean isVisible = false;

        @Override
        public void onCreate(SurfaceHolder holder) {
            super.onCreate(holder);
            boolean isPreview = isPreview();
            Log.d(TAG, "Engine onCreate isPreview:"+isPreview);
            if(!isPreview)
            {
                try {
                    UnityPlayer.UnitySendMessage("AndroidSetting","HideSetting",null);
                }catch (Exception ignored){
                }
            }
            setTouchEventsEnabled(true);
            try {
                if (holder.getSurface() != null && holder.getSurface().isValid()) {
                    service.displayChanged(0, holder.getSurface());
                    service.windowFocusChanged(true);
                }
            } catch (Exception e) {
                Log.e(TAG, "Failed to initialize UnityPlayer", e);
            }
        }

        @Override
        public void onTouchEvent(MotionEvent motionEvent) {
            if (service != null) {
                try {
                    service.injectEvent(motionEvent);
                } catch (Exception e) {
                    Log.e(TAG, "Failed to inject touch event", e);
                }
            }
        }

        @Override
        public void onVisibilityChanged(boolean visible) {
            super.onVisibilityChanged(visible);
            Log.i(TAG, "Visibility changed: " + visible);
            isVisible = visible;

            if (service != null) {
                try {
                    if (visible) {
                        service.onResume();
                        SurfaceHolder holder = getSurfaceHolder();
                        if (holder != null && holder.getSurface() != null && holder.getSurface().isValid()) {
                            service.displayChanged(0, holder.getSurface());
                        }
                    } else {
                        service.onPause();
                    }
                    service.windowFocusChanged(visible);
                } catch (Exception e) {
                    Log.e(TAG, "Error during visibility change", e);
                }
            }
        }

        @Override
        public void onDestroy() {
            super.onDestroy();
            Log.d(TAG, "Engine onDestroy");
            if (service != null) {
                try {
                    service.onUnityPlayerQuitted();
                    service = null; // Help GC
                } catch (Exception e) {
                    Log.e(TAG, "Failed to destroy UnityPlayer", e);
                }
            }
        }
    }
}